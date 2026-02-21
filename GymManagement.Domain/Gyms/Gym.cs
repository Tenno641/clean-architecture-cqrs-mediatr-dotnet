using System.Data;
using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.Subscriptions.Events;

namespace GymManagement.Domain.Gyms;

public class Gym : Entity
{
    public string Name { get; private set; }
    public Subscription Subscription { get; private set; }
    public List<Room> Rooms { get; private set; }

    public Guid SubscriptionId { get; private set; }
    public int MaxRooms { get; private set; }

    public Gym(string name, Guid subscriptionId, int maxRoomses, Guid? id = null): base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        SubscriptionId = subscriptionId;

        MaxRooms = maxRoomses;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (Rooms.Contains(room))
            throw new ConstraintException("Room already exists in gym");

        if (Rooms.Count >= MaxRooms)
            return GymErrors.CannotHaveMoreRooms;

        Rooms.Add(room);
        

        return Result.Success;
    }

    private Gym() { }
}
