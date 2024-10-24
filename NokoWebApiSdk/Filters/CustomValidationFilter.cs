using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Converters;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Filters;

public class ReportInvalidField
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required]
    [JsonPropertyName("errors")]
    public string[] Errors { get; set; }
}

public class ReportInvalidFields
{
    [Required]
    [JsonPropertyName("fields")]
    public IEnumerable<ReportInvalidField> Fields { get; set; }
}

public class ReportInvalidFieldsMessageBody : MessageBody<ReportInvalidFields>;

public class CustomValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errors = context.ModelState
            .Where(x => x.Value is { Errors.Count: > 0 })
            .Select(kv => new ReportInvalidField
            {
                Name = NokoWebTransformText.ToCamelCase(kv.Key),
                Errors = kv.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? []
            })
            .ToArray();

        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.ContentType = "application/json";
        
        var statusCode = (HttpStatusCode)response.StatusCode;

        var reports = new ReportInvalidFields
        {
            Fields = errors,
        };
        
        var messageBody = new ReportInvalidFieldsMessageBody
        {
            StatusOk = false,
            StatusCode = (int)statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = "One or more validation errors occurred.",
            Data = reports,
        };
        
        var options = new JsonSerializerOptions();
        JsonService.JsonSerializerConfigure(options);

        context.Result = new JsonResult(messageBody, options)
        {
            StatusCode = response.StatusCode
        };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action on executed
    }
}