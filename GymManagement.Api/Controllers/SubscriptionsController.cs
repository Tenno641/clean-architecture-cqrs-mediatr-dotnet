using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        Guid newSubscriptionId = await _mediator.Send(command, cancellationToken);

        SubscriptionResponse response = new SubscriptionResponse(newSubscriptionId, request.SubscriptionType);

        return Ok(response);
    }
}