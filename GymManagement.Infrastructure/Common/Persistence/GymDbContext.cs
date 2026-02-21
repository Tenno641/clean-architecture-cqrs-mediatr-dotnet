using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common;
using GymManagement.Domain.Common.Events;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Sessions;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymDbContext : DbContext, IUnitOfWork
{
    private readonly HttpContextAccessor _httpContextAccessor;
    public GymDbContext(DbContextOptions<GymDbContext> options, HttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Username=postgres; Password=password; Database=GymDB");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitChangesAsync()
    {
        List<IDomainEvent> events = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(@event => @event)
            .ToList();
        
        AddDomainEventsToProcessingQueue(events);
        
        await SaveChangesAsync();
    }

    private void AddDomainEventsToProcessingQueue(List<IDomainEvent> events)
    {
        Queue<IDomainEvent> domainEventsQueue = 
            _httpContextAccessor.HttpContext.Items.TryGetValue("DomainEvents", out object? domainValues)
            && domainValues is Queue<IDomainEvent> existingDomainEventsQueue
                ? existingDomainEventsQueue
                : new Queue<IDomainEvent>();

        events.ForEach(@event => domainEventsQueue.Enqueue(@event));
        
        _httpContextAccessor.HttpContext.Items.Add("DomainEvents", domainEventsQueue);
    }
}