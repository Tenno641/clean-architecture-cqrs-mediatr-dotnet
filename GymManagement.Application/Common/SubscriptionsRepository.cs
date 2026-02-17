using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    public Task CreateSubscription(Subscription subscription)
    {
        return Task.CompletedTask;
    }
}