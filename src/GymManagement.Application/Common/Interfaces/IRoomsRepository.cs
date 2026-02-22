using System.Reflection.Metadata;
using GymManagement.Domain.Rooms;

namespace GymManagement.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task CreateRoomAsync(Room room);
    Task<Room?> GetRoomById(Guid id);
    Task<IEnumerable<Room>> ListRoomsByGymIdAsync(Guid gymId);
    void RemoveRange(IEnumerable<Room> rooms);
}