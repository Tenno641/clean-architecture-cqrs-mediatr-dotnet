using ErrorOr;
using GymManagement.Application.Authorization;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

[Authorize(Permissions = "gyms:create")]
public record CreateGymCommand(Guid SubscriptionId, string Name) : IRequest<ErrorOr<Guid>>;