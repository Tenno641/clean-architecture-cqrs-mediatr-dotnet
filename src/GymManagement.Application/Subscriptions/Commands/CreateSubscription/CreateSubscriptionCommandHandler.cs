using ErrorOr;
using GymManagement.Application.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.SharedKernel.Enums;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        Subscription subscription = new Subscription(request.SubscriptionType, request.AdminId);

        await _subscriptionsRepository.CreateSubscriptionAsync(subscription);

        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}