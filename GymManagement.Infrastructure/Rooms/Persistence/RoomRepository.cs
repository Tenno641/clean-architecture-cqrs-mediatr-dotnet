using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Rooms.Persistence;

public class RoomRepository : IRoomsRepository
{
    private readonly GymDbContext _context;

    public RoomRepository(GymDbContext context)
    {
        _context = context;
    }

    public async Task CreateRoomAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
    }
    public async Task<Room?> GetRoomById(Guid id)
    {
        return await _context.Rooms
            .Include(room => room.Sessions)
            .FirstOrDefaultAsync(room => room.Id == id);
    }
    public async Task<IEnumerable<Room>> ListRoomsByGymIdAsync(Guid gymId)
    {
        IEnumerable<Room> rooms = await _context.Rooms
            .Where(room => room.GymId == gymId)
            .ToListAsync();

        return rooms;
    }
    public void RemoveRange(IEnumerable<Room> rooms)
    {
        _context.RemoveRange(rooms);
    }
}