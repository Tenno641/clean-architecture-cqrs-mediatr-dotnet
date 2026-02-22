using ErrorOr;
using GymManagement.Application.Authentication.Commands;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Authentication.Queries;
using GymManagement.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ApiController 
{
    private readonly ISender _sender;
    
    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterCommand command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

        ErrorOr<AuthenticationResult> result = await _sender.Send(command);

        return result.Match(
            onValue: value => Ok(ToAuthenticationResponse(value)),
            onError: Problem);
    }
    
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new LoginQuery(request.Email, request.Password);

        ErrorOr<AuthenticationResult> authResult = await _sender.Send(query);

        if (authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials)
        {
            return Problem(
                detail: authResult.FirstError.Description,
                statusCode: StatusCodes.Status401Unauthorized);
        }

        return authResult.Match(
            onValue: value => Ok(ToAuthenticationResponse(value)),
            Problem);
    }
    
    private static AuthenticationResponse ToAuthenticationResponse(AuthenticationResult result)
    {
        AuthenticationResponse response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
    
            return response;
    }
}