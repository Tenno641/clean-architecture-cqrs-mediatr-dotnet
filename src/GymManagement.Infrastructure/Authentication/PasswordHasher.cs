using ErrorOr;
using GymManagement.Domain.Common.Interfaces;

namespace GymManagement.Infrastructure.Authentication;

public partial class PasswordHasher : IPasswordHasher
{
    public ErrorOr<string> HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool IsCorrectPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}