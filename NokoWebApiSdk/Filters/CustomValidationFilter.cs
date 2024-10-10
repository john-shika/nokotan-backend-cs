using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils;
using NokoWebApiSdk.Utils.Net;

namespace NokoWebApiSdk.Filters;

public class CustomValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errors = context.ModelState
            .Where(x => x.Value is { Errors.Count: > 0 })
            .ToDictionary(
                keyValuePair => NokoWebTransform.ToCamelCase(keyValuePair.Key), 
                keyValuePair => keyValuePair.Value?.Errors.Select(e => e.ErrorMessage).ToArray());

        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCodes.BadRequest;
        response.ContentType = "application/json";

        var messageBody = new MessageBody<Dictionary<string, string[]?>>
        {
            StatusOk = false,
            StatusCode = response.StatusCode,
            Status = HttpStatusText.FromCode((HttpStatusCodes)response.StatusCode),
            Timestamp = NokoWebCommon.GetDateTimeUtcNowInMilliseconds(),
            Message = "One or more validation errors occurred.",
            Data = errors,
        };
        
        var jsonSerializerOptions = new JsonSerializerOptions
        {
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