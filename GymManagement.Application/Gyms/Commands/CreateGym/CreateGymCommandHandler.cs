using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Guid>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);

        if (subscription is null)
            return Error.NotFound(
                code: "Subscription.NotFound",
                description: "Subscription is not found");

        Gym gym = new Gym(request.Name, request.SubscriptionId, subscription.GetMaxRooms());

        ErrorOr<Success> result = subscription.AddGym(gym);

        if (result.IsError)
            return result.Errors;

        await _gymsRepository.CreateGymAsync(gym);
        
        await _unitOfWork.CommitChangesAsync();

        return gym.Id;
    }
}