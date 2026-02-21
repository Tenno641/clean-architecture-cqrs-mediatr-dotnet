using GymManagement.Domain.Common.Events;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymManagement.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IPublisher _publisher;
    private readonly GymDbContext _dbContext;
    
    public EventualConsistencyMiddleware(RequestDelegate next, IPublisher publisher, GymDbContext dbContext)
    {
        _next = next;
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnCompleted(async () =>
        {
            IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (context.Items.TryGetValue("DomainEvents", out object? domainEvents) && domainEvents is Queue<IDomainEvent> existingDomainEvents)
                {
                    while (existingDomainEvents.TryDequeue(out IDomainEvent? @event))
                    {
                        await _publisher.Publish(@event);
                    }
                    await _dbContext.Database.CommitTransactionAsync();
                }
            }
            catch (Exception e)
            {
                // Inform the user about the error.
            }
            finally
            {
                await _dbContext.DisposeAsync();
            }
            
        });

        await _next(context);
    }
}