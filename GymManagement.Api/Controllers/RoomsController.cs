using ErrorOr;
using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("gyms/{gymId:guid}/[controller]")]
public class RoomsController : ApiController
{
    private readonly ISender _sender;

    public RoomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid gymId, CreateRoom request)
    {
        CreateRoomCommand command = new CreateRoomCommand(gymId, request.Name);

        ErrorOr<Guid> result = await _sender.Send(command);

        return result.Match(
            onValue: roomId => Ok(roomId),
            onError: errors => Problem(errors));
    }
}