using DNQ.DataFeed.Application.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace DNQ.DataFeed.Api.Middlewares;

public static class ExceptionHandlerExtensions
{
    public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.Use(WriteDevelopmentResponse);
        }
        else
        {
            app.Use(WriteProductionResponse);
        }
    }

    private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
        => WriteResponse(httpContext, includeDetails: true);

    private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
        => WriteResponse(httpContext, includeDetails: false);

    private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
    {
        // Try and retrieve the error from the ExceptionHandler middleware
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var ex = exceptionDetails?.Error;

        // Should always exist, but best to be safe!
        if (ex != null)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCode(ex),
                Title = Title(ex),
                Detail = ex.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            problem.Extensions["traceId"] = TraceId(httpContext);

            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = (int) problem.Status;
            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }

    private static int StatusCode(Exception exception)
    {
        return exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            ValidationException => (int)HttpStatusCode.BadRequest,
            DomainException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }

    private static string Title(Exception exception)
    {
        return exception switch
        {
            NotFoundException => "Resource Not Found",
            ValidationException => "Validation Error",
            DomainException => "Business Error",
            _ => "An Unexpected Error Occurred"
        };
    }

    private static string? TraceId(HttpContext httpContext)
    {
        return Activity.Current?.Id ?? httpContext?.TraceIdentifier;
    }
}