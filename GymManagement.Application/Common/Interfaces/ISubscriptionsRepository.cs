using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task CreateSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetSubscriptionByIdAsync(Guid id);
}