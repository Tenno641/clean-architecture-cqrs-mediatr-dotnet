using ErrorOr;
using GymManagement.Application.Authentication.Commands;
using MediatR;

namespace GymManagement.Application.Authentication;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<string>>
{
    public Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}