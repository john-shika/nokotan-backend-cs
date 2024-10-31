using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NokoWebApiSdk.Cores.Networking;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Filters;

public class ReportInvalidField(string name, string[] errors)
{
    public ReportInvalidField() : this("", [])
    {
        // do nothing...
    }

    [Required] 
    [JsonPropertyName("name")] 
    public string Name { get; set; } = name;

    [Required]
    [JsonPropertyName("errors")]
    public string[] Errors { get; set; } = errors;
}

public class ReportInvalidFields(IEnumerable<ReportInvalidField> fields)
{
    public ReportInvalidFields() : this([])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("fields")]
    public IEnumerable<ReportInvalidField> Fields { get; set; } = fields;
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
                Name = NokoTransformText.ToCamelCase(kv.Key),
                Errors = kv.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? []
            })
            .ToArray();

        var response = context.HttpContext.Response;
        response.StatusCode = (int)NokoHttpStatusCodes.BadRequest;
        response.ContentType = "application/json";
        
        var statusCode = (NokoHttpStatusCodes)response.StatusCode;

        var reports = new ReportInvalidFields
        {
            Fields = errors,
        };
        
        var messageBody = new ReportInvalidFieldsMessageBody
        {
            StatusOk = false,
            StatusCodes = statusCode,
            Status = statusCode.ToString(),
            Timestamp = NokoCommonMod.GetDateTimeUtcNow(),
            Message = "One or more validation errors occurred.",
            Data = reports,
        };
        
        var options = new JsonSerializerOptions();
        JsonSerializerService.Apply(options);

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