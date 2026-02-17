namespace GymManagement.Application.Services;

public class SubscriptionsWriteWriteService : ISubscriptionsWriteService
{
    public Guid CreateSubscription(string subscriptionType, Guid adminId)
    {
        return Guid.NewGuid();
    }
}