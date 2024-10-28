using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Converters;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Filters;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        var response = context.HttpContext.Response;
        response.StatusCode = (int)NokoWebHttpStatusCode.InternalServerError;
        response.ContentType = "application/json";
        
        var statusCode = (NokoWebHttpStatusCode)response.StatusCode;

        var messageBody = new EmptyMessageBody
        {
            StatusOk = false,
            StatusCode = (int)statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = exception.Message,
            Data = null,
        };

        var options = new JsonSerializerOptions();
        JsonSerializerService.Apply(options);

        context.Result = new JsonResult(messageBody, options)
        {
            StatusCode = response.StatusCode
        };

        context.ExceptionHandled = true;
    }
}

public static class HttpExceptionMiddleware
{
    // Func<HttpContext,Func<Task>,Task> middleware
    // Func<HttpContext,RequestDelegate,Task> middleware
    public static async Task Handler(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.StatusCode = (int)NokoWebHttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
        
            var statusCode = (NokoWebHttpStatusCode)response.StatusCode;
            var messageBody = new EmptyMessageBody
            {
                StatusOk = false,
                StatusCode = (int)statusCode,
                Status = statusCode.ToString(),
                Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
                Message = ex.Message,
                Data = null,
            };
            
            var options = new JsonSerializerOptions();
            JsonSerializerService.Apply(options);

            var jsonResponse = JsonSerializer.Serialize(messageBody, options);
            await response.WriteAsync(jsonResponse);

            // Mark the exception as handled
            // context.ExceptionHandled = true; // Not needed for middleware
        }
    }
}