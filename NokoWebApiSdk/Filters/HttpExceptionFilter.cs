using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils;
using NokoWebApiSdk.Utils.Net;

namespace NokoWebApiSdk.Filters;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCodes.InternalServerError;
        response.ContentType = "application/json";

        var messageBody = new MessageBody<object>
        {
            StatusOk = false,
            StatusCode = response.StatusCode,
            Status = HttpStatusText.FromCode((HttpStatusCodes)response.StatusCode),
            Timestamp = NokoWebCommon.GetDateTimeUtcNowInMilliseconds(),
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