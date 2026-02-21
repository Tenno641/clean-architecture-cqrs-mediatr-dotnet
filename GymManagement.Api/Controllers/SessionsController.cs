using ErrorOr;
using GymManagement.Application.Sessions.Commands.CreateSession;
using GymManagement.Contracts.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("rooms/{roomId:guid}/[controller]")]
public class SessionsController : ApiController
{
    private readonly ISender _mediator;

    public SessionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid roomId, CreateSession request)
    {
        CreateSessionCommand command = new CreateSessionCommand(roomId, request.Date, request.StartTime, request.Type, request.Duration);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.Match(
            onValue: sessionId => Ok(sessionId),
            onError: errors => Problem(errors));
    }
}