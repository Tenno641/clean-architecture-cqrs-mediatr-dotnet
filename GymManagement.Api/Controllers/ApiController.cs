using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

public class ApiController : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        IActionResult problem = Problem(statusCode: statusCode, title: error.Description, detail: error.Code);

        return problem;
    }
}