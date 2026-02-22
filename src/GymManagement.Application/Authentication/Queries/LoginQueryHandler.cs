using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Users;
using MediatR;

namespace GymManagement.Application.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public LoginQueryHandler(IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return AuthenticationErrors.InvalidCredentials;

        if (!user.IsCorrectPassword(request.Password, _passwordHasher))
            return AuthenticationErrors.InvalidCredentials;

        return new AuthenticationResult(user, _tokenGenerator.GenerateToken(user));
    }
}