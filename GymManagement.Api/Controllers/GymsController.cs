using GymManagement.Contracts.Gyms;
using MediatR;
using ErrorOr;
using GymManagement.Application.Gyms.Commands.CreateGym;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("subscriptions/{subscriptionId:guid}/[controller]")]
public class GymsController : ApiController
{
    private readonly ISender _sender;

    public GymsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AddGym(Guid subscriptionId, CreateGym request, CancellationToken cancellationToken)
    {
        CreateGymCommand command = new CreateGymCommand(subscriptionId, request.Name);

        ErrorOr<Guid> result = await _sender.Send(command, cancellationToken);

        return result.Match(
            onValue: gymId => Ok(gymId),
            onError: errors => Problem(errors));
    }
}