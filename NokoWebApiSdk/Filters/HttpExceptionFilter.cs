using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Filters;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = "application/json";
        
        var statusCode = (HttpStatusCode)response.StatusCode;

        var messageBody = new MessageBody<object>
        {
            StatusOk = false,
            StatusCode = (int)statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = exception.Message,
            Data = null,
        };

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        context.Result = new JsonResult(messageBody, jsonSerializerOptions)
        {
            StatusCode = response.StatusCode
        };

        context.ExceptionHandled = true;
    }
}