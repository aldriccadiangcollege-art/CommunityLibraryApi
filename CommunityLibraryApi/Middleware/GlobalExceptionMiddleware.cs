using System.Net;
using System.Text.Json;
using CommunityLibrary.API.Exceptions;

namespace CommunityLibrary.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            NotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
            ConflictException => ((int)HttpStatusCode.Conflict, exception.Message),
            _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred on the server.")
        };

        context.Response.StatusCode = statusCode;
        var response = new { status = statusCode, error = message };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}