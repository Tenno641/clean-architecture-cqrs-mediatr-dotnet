using GymManagement.Domain.Sessions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task CreateSession(Session session);
}