using System.Text.Json;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        var statusCode = (HttpStatusCode)context.Response.StatusCode;

        var messageBody = new MessageBody<object>
        {
            StatusOk = false,
            StatusCode = (int)statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = "An unexpected error occurred.",
            Data = null,
        };
        
        var errorJson = JsonSerializer.Serialize(messageBody);
        return context.Response.WriteAsync(errorJson);
    }
}
