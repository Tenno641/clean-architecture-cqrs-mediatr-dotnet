using GymManagement.Application.Common;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly List<Subscription> _subscriptions = [];
    public async Task CreateSubscriptionAsync(Subscription subscription)
    {
        _subscriptions.Add(subscription);
        
        await Task.CompletedTask;
    }
    public async Task<Subscription?> GetSubscriptionByIdAsync(Guid id)
    {
        Subscription? subscription = _subscriptions.Find(subscription => subscription.Id == id);

        return await Task.FromResult(subscription);
    }
}