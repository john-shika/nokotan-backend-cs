using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Converters;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Filters;

public class CustomValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errors = context.ModelState
            .Where(x => x.Value is { Errors.Count: > 0 })
            .ToDictionary(
                keyValuePair => NokoWebTransformText.ToCamelCase(keyValuePair.Key), 
                keyValuePair => keyValuePair.Value?.Errors.Select(e => e.ErrorMessage).ToArray());

        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.ContentType = "application/json";
        
        var statusCode = (HttpStatusCode)response.StatusCode;

        var messageBody = new MessageBody<Dictionary<string, string[]?>>
        {
            StatusOk = false,
            StatusCode = (int)statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = "One or more validation errors occurred.",
            Data = errors,
        };
        
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonDateTimeConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        context.Result = new JsonResult(messageBody, jsonSerializerOptions)
        {
            StatusCode = response.StatusCode
        };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action on executed
    }
}