using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsWriteService _subscriptionsWriteService;
    
    public SubscriptionsController(ISubscriptionsWriteService subscriptionsWriteService)
    {
        _subscriptionsWriteService = subscriptionsWriteService;
    }
    
    [HttpPost]
    public IActionResult Create(CreateSubscription request)
    {
        Guid newSubscriptionId = _subscriptionsWriteService.CreateSubscription(request.SubscriptionType.ToString(), request.AdminId);

        SubscriptionResponse response = new SubscriptionResponse(newSubscriptionId, request.SubscriptionType);

        return Ok(response);
    }
}