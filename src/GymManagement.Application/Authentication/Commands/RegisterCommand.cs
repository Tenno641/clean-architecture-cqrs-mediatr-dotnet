using MediatR;
using ErrorOr;

namespace GymManagement.Application.Authentication;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password): IRequest<ErrorOr<string>>;