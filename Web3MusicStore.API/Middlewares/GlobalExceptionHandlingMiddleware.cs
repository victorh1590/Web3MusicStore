using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web3MusicStore.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly IHostEnvironment _environment;
    // private readonly ILogger _logger;

    public GlobalExceptionHandlingMiddleware(IHostEnvironment environment)
    {
        _environment = environment;
    }

    private static ProblemDetails GenerateProblemDetails(int status, string type, string title, string details)
    {
        return new ProblemDetails
        {
            Status = status,
            Type = type,
            Title = title,
            Detail = details
        };
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // _logger.LogInformation(ex, "An error occurred when processing the request.");
            ProblemDetails error;
            context.Response.ContentType = "application/problem+json";
            if (_environment.IsDevelopment())
            {
                context.Response.StatusCode = ex switch
                {
                    ArgumentException 
                        or DbUpdateException 
                        or DbUpdateConcurrencyException => (int)HttpStatusCode.BadRequest,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                error = GenerateProblemDetails(
                    context.Response.StatusCode,
                    ex.GetType().Name,
                    "An error occurred when processing the request.",
                    ex.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error = GenerateProblemDetails(
                    (int)HttpStatusCode.InternalServerError,
                    "Internal Server Error",
                    "Internal Server Error",
                    "An internal server error has occurred.");
            }
            
            var json = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(json);
        }
    }
}