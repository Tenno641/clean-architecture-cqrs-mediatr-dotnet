using GymManagement.Domain.Users;

namespace GymManagement.Application.Common.Interfaces;

public interface IUserRepository
{
    public Task AddUserAsync(User user);

    public Task<bool> ExistsByEmailAsync(string email);

    public Task<User?> GetByEmailAsync(string email);

    public Task<User?> GetByIdAsync(Guid userId);
}