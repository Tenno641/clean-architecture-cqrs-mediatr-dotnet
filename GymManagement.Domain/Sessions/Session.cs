using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.Sessions;

public class Session
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public string Type { get; private set; }
    
    public Guid RoomId { get; set; }
    
    public Room Room { get; private set; }
    
    public Session(DateOnly date, TimeOnly startTime, int duration, string type, Guid? id = null)
    {
        Date = date;
        StartTime = startTime;
        EndTime = StartTime.AddHours(duration);
        Type = type;
        Id = id ?? Guid.CreateVersion7();
    }
    
    private Session() { }
}