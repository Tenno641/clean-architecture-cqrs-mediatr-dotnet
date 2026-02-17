using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common;

public interface ISubscriptionsRepository
{
    Task CreateSubscription(Subscription subscription);
}