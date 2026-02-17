using ErrorOr;
using GymManagement.Application.Common;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    // private readonly IUnitOfWork _unitOfWork;
    
    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        // _unitOfWork = unitOfWork;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        Subscription subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            SubscriptionType = request.SubscriptionType
        };

        // await _unitOfWork.SaveChangesAsync();
        
        await _subscriptionsRepository.CreateSubscriptionAsync(subscription);

        return subscription;
    }
}