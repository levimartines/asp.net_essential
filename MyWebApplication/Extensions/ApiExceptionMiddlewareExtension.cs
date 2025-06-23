using Microsoft.AspNetCore.Diagnostics;
using MyWebApplication.Models;

namespace MyWebApplication.Extensions;

public static class ApiExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error => error.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = contextFeature.Error.Message,
                    StackTrace = contextFeature.Error.StackTrace
                }.ToString());
        }));

    }
}