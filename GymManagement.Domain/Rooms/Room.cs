using System.Data;
using ErrorOr;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Sessions;

namespace GymManagement.Domain.Rooms;

public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public Gym Gym { get; private set; }
    public int MaxDailySessions { get; private set; }
    
    public Guid GymId { get; private set; }
    public List<Session> Sessions { get; private set; }

    public Room(string name, Guid gymId, int maxDailySessions, Guid? id = null)
    {
        Id = id ?? Guid.CreateVersion7();
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySessions;
    }

    public ErrorOr<Success> AddSession(Session session)
    {
        if (SessionAlreadyExists(session))
            throw new ConstraintException("Session already exists");

        if (SessionOverlap(session))
            return SessionErrors.OverlappingSessions;
                
        if (Sessions.Count >= MaxDailySessions)
            return SessionErrors.CannotHaveMoreDailySessions;
        
        Sessions.Add(session);
        
        return Result.Success;
    }

    private bool SessionAlreadyExists(Session session) => Sessions.Any(s => s.Id == session.Id);

    private bool SessionOverlap(Session session) =>
        Sessions.Any(s =>
            s.Date == session.Date &&
            s.StartTime < session.EndTime &&
            session.StartTime < s.EndTime);
    
    private Room() { }
}