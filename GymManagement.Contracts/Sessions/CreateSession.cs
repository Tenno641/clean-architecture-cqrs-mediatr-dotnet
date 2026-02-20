namespace GymManagement.Contracts.Sessions;

public record CreateSession(Guid RoomId, DateOnly Date, TimeOnly StartTime, string Type, int Duration);