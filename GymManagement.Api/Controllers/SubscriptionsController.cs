using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsService _subscriptionsService;
    
    public SubscriptionsController(ISubscriptionsService subscriptionsService)
    {
        _subscriptionsService = subscriptionsService;
    }
    
    [HttpPost]
    public IActionResult Create(CreateSubscription request)
    {
        Guid newSubscriptionId = _subscriptionsService.CreateSubscription(request.SubscriptionType.ToString(), request.AdminId);

        SubscriptionResponse response = new SubscriptionResponse(newSubscriptionId, request.SubscriptionType);

        return Ok(response);
    }
}