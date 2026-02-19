using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.SharedKernel.Enums;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ApiController
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscription request, CancellationToken cancellationToken)
    {
        CreateSubscriptionCommand command = new CreateSubscriptionCommand(request.SubscriptionType, request.AdminId);

        ErrorOr<Subscription> response = await _mediator.Send(command, cancellationToken);

        return response.MatchFirst(
            onValue: subscription => Ok(new SubscriptionResponse(subscription.Id, subscription.SubscriptionType, subscription.AdminId)),
            onFirstError: error => Problem(error));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        GetSubscriptionQuery query = new GetSubscriptionQuery(id);

        ErrorOr<Subscription> result = await _mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            onValue: subscription => Ok(new SubscriptionResponse(subscription.Id, subscription.SubscriptionType, subscription.AdminId)),
            onFirstError: error => Problem(error));
    }
}