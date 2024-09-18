using System.Net;
using System.Text.Json;

using MyApp.Application.Common.Exceptions;

namespace MyApp.WebMvcUI.Middleware;

public class AppExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionMiddleware> _logger;


    public AppExceptionMiddleware(
        RequestDelegate next, 
        ILogger<AppExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }   

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) 
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    #region Helper methods

    private Task HandleExceptionAsync(
        HttpContext context, 
        Exception ex)
    {
        HttpStatusCode statusCode;
        string message;

        switch(ex)
        {
            case MyValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = JsonSerializer.Serialize(validationException.Errors);
                break;
            case TaskCanceledException:
                return Task.CompletedTask;
            case MyNotFoundException notFoundException:
                statusCode= HttpStatusCode.NotFound;
                message = JsonSerializer.Serialize(notFoundException);
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Something went wrong.  Contact Developer.";
                _logger.LogError("Something went wrong: {ex}", ex);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(message);
    }

    #endregion
}
