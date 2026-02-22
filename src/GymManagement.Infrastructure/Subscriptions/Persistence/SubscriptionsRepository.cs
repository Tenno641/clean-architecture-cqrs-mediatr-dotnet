using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly GymDbContext _context;
    public SubscriptionsRepository(GymDbContext context)
    {
        _context = context;
    }
    public async Task CreateSubscriptionAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }
    public async Task<Subscription?> GetSubscriptionByIdAsync(Guid id)
    {
        Subscription? subscription = await _context.Subscriptions
            .Include(subscription => subscription.Gyms)
            .SingleOrDefaultAsync(subscription => subscription.Id == id);

        return subscription;
    }
}