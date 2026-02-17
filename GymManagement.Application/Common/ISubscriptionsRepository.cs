using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common;

public interface ISubscriptionsRepository
{
    Task CreateSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetSubscriptionByIdAsync(Guid id);
}