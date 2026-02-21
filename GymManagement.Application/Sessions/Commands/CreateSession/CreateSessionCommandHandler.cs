using MediatR;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Sessions;

namespace GymManagement.Application.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ErrorOr<Guid>>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSessionCommandHandler(ISessionsRepository sessionsRepository, IUnitOfWork unitOfWork, IRoomsRepository roomsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _unitOfWork = unitOfWork;
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetRoomById(request.RoomId);

        if (room is null)
            return Error.Validation(description: "Room is not found");

        Session session = new Session(request.Date, request.StartTime, request.Duration, request.Type);

        ErrorOr<Success> result = room.AddSession(session);

        if (result.IsError)
            return result.Errors;

        await _sessionsRepository.CreateSession(session);

        await _unitOfWork.CommitChangesAsync();

        return session.Id;
    }
}