using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DNQ.DataFeed.Api.Middlewares;

public static class ApiBehaviorOptionsExtensions
{
    public static IServiceCollection ConfigureCustomModelValidationResponse(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred.",
                    Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}"
                };

                problemDetails.Extensions["traceId"] = TraceId(context.HttpContext);

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });

        return services;
    }

    private static string? TraceId(HttpContext httpContext)
    {
        return Activity.Current?.Id ?? httpContext?.TraceIdentifier;
    }
}
