using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApplication.Filters;

public class ApiExceptionFilter(ILogger logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, $"Error: {context.Exception.Message}");
        context.Result = new ObjectResult("Internal Server Error")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}