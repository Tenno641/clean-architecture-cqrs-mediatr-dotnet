using GymManagement.SharedKernel.Enums;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    public Guid Id { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
}