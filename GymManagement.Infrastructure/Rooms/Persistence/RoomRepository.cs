using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Infrastructure.Common.Persistence;

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
}