using GymManagement.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace GymManagement.Application.Authentication.Queries;

public record LoginQuery(string Email, string Password): IRequest<ErrorOr<AuthenticationResult>>;