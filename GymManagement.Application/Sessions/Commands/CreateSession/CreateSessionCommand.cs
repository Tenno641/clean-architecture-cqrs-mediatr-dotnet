using MediatR;
using ErrorOr;

namespace GymManagement.Application.Sessions.Commands.CreateSession;

public record CreateSessionCommand(Guid RoomId, DateOnly Date, TimeOnly StartTime, string Type, int Duration): IRequest<ErrorOr<Guid>>;