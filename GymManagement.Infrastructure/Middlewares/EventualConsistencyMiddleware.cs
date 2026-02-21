using GymManagement.Domain.Common.Events;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymManagement.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware 
{
    private readonly RequestDelegate _next;

    public EventualConsistencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymDbContext gymDbContext)
    {
        context.Response.OnCompleted(async () =>
        {
            IDbContextTransaction transaction = await gymDbContext.Database.BeginTransactionAsync();
            try
            {
                if (context.Items.TryGetValue("DomainEvents", out object? domainEvents) && domainEvents is Queue<IDomainEvent> existingDomainEvents)
                {
                    while (existingDomainEvents.TryDequeue(out IDomainEvent? @event))
                    {
                        await publisher.Publish(@event);
                    }
                    await gymDbContext.Database.CommitTransactionAsync();
                }
            }
            catch (Exception e)
            {
                // Inform the user about the error.
            }
            finally
            {
                await gymDbContext.DisposeAsync();
            }

        });

        await _next(context);
    }
}