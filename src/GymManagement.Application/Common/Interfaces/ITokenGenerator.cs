using GymManagement.Domain.Users;

namespace GymManagement.Application.Common.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}