using System.Data;
using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions.Events;
using GymManagement.SharedKernel.Enums;

namespace GymManagement.Domain.Subscriptions;

public class Subscription : Entity
{
    public Guid AdminId { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }

    public List<Gym> Gyms { get; private set; }

    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null): base(id ?? Guid.CreateVersion7())
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
    }

    private static int GetMaxNumberOfGyms(SubscriptionType subscriptionType)
    {
        return subscriptionType switch
        {
            SubscriptionType.Free => 1,
            SubscriptionType.Starter => 3,
            SubscriptionType.Pro => int.MaxValue,
            _ => throw new InvalidOperationException()
        };
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (Gyms.Contains(gym))
            throw new ConstraintException("Gym already belongs to this subscription");

        if (Gyms.Count >= GetMaxNumberOfGyms(SubscriptionType))
            return SubscriptionErrors.CannotHaveMoreGyms;

        Gyms.Add(gym);

        return Result.Success;
    }

    public int GetMaxRooms() => SubscriptionType switch
    {
        SubscriptionType.Free => 1,
        SubscriptionType.Starter => 3,
        SubscriptionType.Pro => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType switch
    {
        SubscriptionType.Free => 4,
        SubscriptionType.Starter => int.MaxValue,
        SubscriptionType.Pro => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public ErrorOr<Deleted> DeleteGym(Guid id)
    {
        Gym? gym = Gyms.FirstOrDefault(g => g.Id == id);

        if (gym is null)
            return SubscriptionErrors.GymNotFound;

        Gyms.Remove(gym);
        
        DomainEvents.Add(new GymDeletedEvent(Id));
        
        return Result.Deleted;
    }

    private Subscription() { }
}