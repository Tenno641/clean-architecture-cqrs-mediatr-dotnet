using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymDbContext _gymDbContext;

    public GymsRepository(GymDbContext gymDbContext)
    {
        _gymDbContext = gymDbContext;
    }

    public async Task CreateGymAsync(Gym gym)
    {
        await _gymDbContext.Gyms.AddAsync(gym);
    }
    public async Task<Gym?> GetGymById(Guid id)
    {
        return await _gymDbContext.Gyms
            .Include(gym => gym.Rooms)
            .FirstOrDefaultAsync(gym => gym.Id == id);
    }
    public async Task DeleteGymAsync(Guid id)
    {
        Gym? gym = await _gymDbContext.Gyms
            .FirstOrDefaultAsync(gym => gym.Id == id);
        
        if (gym is not null)
            _gymDbContext.Gyms.Remove(gym);
    }
}