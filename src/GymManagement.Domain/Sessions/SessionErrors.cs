using ErrorOr;

namespace GymManagement.Domain.Sessions;

public static class SessionErrors
{
    public readonly static Error CannotHaveMoreDailySessions = Error.Validation(
        code: "Room.CannotHaveMoreDailySessions",
        description: "Subscription doesn't allow adding more daily sessions");

    public readonly static Error OverlappingSessions = Error.Validation(
        code: "Room.SessionOverlap",
        description: "Added Session will overlap with existing one");
}