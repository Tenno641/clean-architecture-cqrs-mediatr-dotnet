using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymDbContext : DbContext, IUnitOfWork
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Username=postgres; Password=password; Database=GymDB");
        base.OnConfiguring(optionsBuilder);
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }
}