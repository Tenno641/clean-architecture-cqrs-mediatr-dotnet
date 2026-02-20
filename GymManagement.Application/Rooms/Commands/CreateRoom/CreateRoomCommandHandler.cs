using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Guid>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    
    public CreateRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork, IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymById(request.GymId);

        if (gym is null)
            return Error.NotFound(
                code: "Gym.NotFound",
                description: "Can't find gym");

        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(gym.SubscriptionId);

        if (subscription is null)
            return Error.Unexpected("Gym Created without subscription");

        Room room = new Room(request.Name, gym.Id, subscription.GetMaxDailySessions(), gym.Id);

        ErrorOr<Success> result = gym.AddRoom(room);

        if (result.IsError)
            return result.Errors;

        await _roomsRepository.CreateRoomAsync(room);

        await _unitOfWork.CommitChangesAsync();

        return room.Id;
    }
}