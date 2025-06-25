using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApplication.Filters;

public class ApiLoggingFilter(ILogger<ApiLoggingFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // BEFORE ACTION
        logger.LogInformation("OnActionExecuting");
        logger.LogInformation("#################");
        var now = DateTime.Now;
        logger.LogInformation("{ToLongDateString} {ToLongTimeString}", now.ToLongDateString(), now.ToLongTimeString());
        logger.LogInformation("#################");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // AFTER ACTION
        logger.LogInformation("OnActionExecuted");
        logger.LogInformation("#################");
        var now = DateTime.Now;
        logger.LogInformation("{ToLongDateString} {ToLongTimeString}", now.ToLongDateString(), now.ToLongTimeString());
        logger.LogInformation("Status Code: {ResponseStatusCode}", context.HttpContext.Response.StatusCode);
    }
}