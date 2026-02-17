using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscription request, CancellationToken cancellationToken)
    {
        CreateSubscriptionCommand command = new CreateSubscriptionCommand(request.SubscriptionType.ToString(), request.AdminId);

        ErrorOr<Subscription> response = await _mediator.Send(command, cancellationToken);

        return response.Match(
            onValue: subscription => Ok(new SubscriptionResponse(subscription.Id, SubscriptionType.Free)),
            onError: _ => Problem());
    }
}