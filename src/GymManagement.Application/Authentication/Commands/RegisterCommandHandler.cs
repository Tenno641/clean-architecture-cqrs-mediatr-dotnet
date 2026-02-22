using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Users;
using MediatR;

namespace GymManagement.Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            return Error.Conflict(description: "User Already Exists");
        }

        ErrorOr<string> hashResult = _passwordHasher.HashPassword(request.Password);

        if (hashResult.IsError)
            return Error.Failure(description: "Password is invalid");
        
        User user = new User(request.FirstName, request.LastName, request.Email, hashResult.Value);

        await _userRepository.AddUserAsync(user);

        await _unitOfWork.CommitChangesAsync();

        string token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}