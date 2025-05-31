using Microsoft.AspNetCore.Mvc;
using Tutorial10.Application.Utils;

namespace Tutorial10.Api.Extensions;

public static class ControllerUtils
{
    public static IActionResult ResponseFromErrResult<T>(
        this ControllerBase controller, Result<T> result)
    {
        if (result.IsOk)
        {
            throw new ArgumentException("Cannot map Error response from OK result");
        }
         
        var err = result.Error;
 
        return err.Type switch
        {
            ErrorType.NotFound => controller.NotFound(err.Message),
            ErrorType.Conflict => controller.Conflict(err.Message),
            ErrorType.BadRequest => controller.BadRequest(err.Message),
            _ => controller.StatusCode(500, err.Message)
        };
    } 
}