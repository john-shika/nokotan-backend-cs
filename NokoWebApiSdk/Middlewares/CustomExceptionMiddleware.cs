﻿using System.Text.Json;
using NokoWebApiSdk.Cores.Networking;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Services;
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.StatusCode = (int)NokoHttpStatusCodes.InternalServerError;
        response.ContentType = "application/json";
        
        var statusCode = (NokoHttpStatusCodes)response.StatusCode;
        var messageBody = new EmptyMessageBody
        {
            StatusOk = false,
            StatusCodes = statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoCommonMod.GetDateTimeUtcNow(),
            Message = ex.Message,
            Data = null,
        };
            
        var options = new JsonSerializerOptions();
        JsonSerializerService.Apply(options);

        var jsonResponse = JsonSerializer.Serialize(messageBody, options);
        await response.WriteAsync(jsonResponse);
    }
}
