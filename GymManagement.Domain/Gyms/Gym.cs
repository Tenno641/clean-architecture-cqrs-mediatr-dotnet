using System.Data;
using ErrorOr;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Gyms;

public class Gym
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public Subscription Subscription { get; private set; }
    public List<Room> Rooms { get; private set; } 
    
    public Guid SubscriptionId { get; private set; }
    private int _maxRooms;

    public Gym(string name, Guid subscriptionId, int maxRooms, Guid? id = null)
    {
        Name = name;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.CreateVersion7();

        Console.WriteLine($"const{maxRooms}");
        _maxRooms = maxRooms;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (Rooms.Contains(room))
            throw new ConstraintException("Room already exists in gym");
        
        if (Rooms.Count >= _maxRooms)
            return GymErrors.CannotHaveMoreRooms;
        
        Rooms.Add(room);

        return Result.Success;
    }
    
    private Gym() { }
}
