using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
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

        ErrorOr<Guid> result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            onValue: guid => Ok(new SubscriptionResponse(guid, request.SubscriptionType)),
            onError: _ => Problem());
    }
}