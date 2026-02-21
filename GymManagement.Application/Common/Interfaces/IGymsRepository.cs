using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task CreateGymAsync(Gym gym);
    Task<Gym?> GetGymById(Guid id);
    Task DeleteGymAsync(Guid id);
}