using Microsoft.AspNetCore.Mvc;

namespace PolyclinicRegistryOffice.ErrorContext;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(this Result<T, Error> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.Error.Type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(result.Error),
            ErrorType.NotFound => new NotFoundObjectResult(result.Error),
            ErrorType.Conflict => new ConflictObjectResult(result.Error),
            _ => new ObjectResult(result.Error) { StatusCode = 500 }
        };
    }
}