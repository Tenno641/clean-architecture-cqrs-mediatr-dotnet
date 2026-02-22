using GymManagement.Domain.Subscriptions;
using GymManagement.SharedKernel.Enums;

namespace TestCommon.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        Subscription subscription = new Subscription(
            subscriptionType: subscriptionType ?? Constants.Constants.Subscription.DefaultSubscriptionType,
            adminId: adminId ?? Constants.Constants.Admin.Id,
            id: id ?? Constants.Constants.Subscription.Id);

        return subscription;
    }
}