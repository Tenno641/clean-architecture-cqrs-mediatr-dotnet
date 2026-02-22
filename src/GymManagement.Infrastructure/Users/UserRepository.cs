using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Users;

public class UserRepository : IUserRepository
{
    private readonly GymDbContext _gymDbContext;
    
    public UserRepository(GymDbContext gymDbContext)
    {
        _gymDbContext = gymDbContext;
    }

    public async Task AddUserAsync(User user)
    {
        await _gymDbContext.Users.AddAsync(user);
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    { 
        return await _gymDbContext.Users.AnyAsync(user => user.Email == email);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        User? user = await _gymDbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

        return user;
    }
    
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        User? user = await _gymDbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

        return user;
    }
}