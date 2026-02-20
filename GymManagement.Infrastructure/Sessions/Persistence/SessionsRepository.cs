using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Sessions;
using GymManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Sessions.Persistence;

public class SessionsRepository : ISessionsRepository
{
    private readonly GymDbContext _context;
    
    public SessionsRepository(GymDbContext context)
    {
        _context = context;
    }

    public async Task CreateSession(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }
}