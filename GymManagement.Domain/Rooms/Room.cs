using GymManagement.Domain.Gyms;

namespace GymManagement.Domain.Rooms;

public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public Gym Gym { get; private set; }
    
    public Guid GymId { get; private set; }

    public Room(string name, Guid gymId, Guid? id = null)
    {
        Id = id ?? Guid.CreateVersion7();
        Name = name;
        GymId = gymId;
    }
    
    private Room() { }
}