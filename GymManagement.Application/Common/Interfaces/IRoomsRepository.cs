using System.Reflection.Metadata;
using GymManagement.Domain.Rooms;

namespace GymManagement.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task CreateRoomAsync(Room room);
    Task<Room?> GetRoomById(Guid id);
}