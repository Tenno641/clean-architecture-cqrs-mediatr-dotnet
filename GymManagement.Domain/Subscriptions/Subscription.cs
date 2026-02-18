using GymManagement.SharedKernel.Enums;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    public Guid AdminId { get; private set; }
    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }
    
    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.CreateVersion7();
    }
    
    private Subscription() { }
}