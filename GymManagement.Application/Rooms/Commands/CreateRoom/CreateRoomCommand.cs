using MediatR;
using ErrorOr;

namespace GymManagement.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(Guid GymId, string Name): IRequest<ErrorOr<Guid>>;