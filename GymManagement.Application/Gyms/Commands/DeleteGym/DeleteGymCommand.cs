using MediatR;
using ErrorOr;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public record DeleteGymCommand(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Unit>>;