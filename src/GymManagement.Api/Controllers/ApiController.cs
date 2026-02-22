using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymManagement.Api.Controllers;

public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
            return Problem();

        return errors.All(error => error.Type == ErrorType.Validation)
            ? ValidationProblem(errors)
            : Problem(errors[0]);
    }

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

    protected IActionResult ValidationProblem(List<Error> errors)
    {
        ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

        foreach (Error error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}