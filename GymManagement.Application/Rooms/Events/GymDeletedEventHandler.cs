using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Subscriptions.Events;
using MediatR;

namespace GymManagement.Application.Rooms.Events;

public class GymDeletedEventHandler : INotificationHandler<GymDeletedEvent>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GymDeletedEventHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
    {
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(GymDeletedEvent notification, CancellationToken cancellationToken)
    {
        IEnumerable<Room> rooms = await _roomsRepository.ListRoomsByGymIdAsync(notification.GymId);

        _roomsRepository.RemoveRange(rooms);

        await _unitOfWork.CommitChangesAsync();
    }
}