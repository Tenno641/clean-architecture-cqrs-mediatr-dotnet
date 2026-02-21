using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Unit>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    
    public DeleteGymCommandHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Unit>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);

        if (subscription is null)
            return Error.NotFound(code: "Subscription.NotFound",
                description: "Subscription is not found");

        ErrorOr<Deleted> result = subscription.DeleteGym(request.GymId);

        if (result.IsError)
            return result.Errors;
        
        await _gymsRepository.DeleteGymAsync(request.GymId);

        await _unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}