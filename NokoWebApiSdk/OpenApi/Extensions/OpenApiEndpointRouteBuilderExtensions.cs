using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Writers;

namespace NokoWebApiSdk.OpenApi.Extensions;

public static class OpenApiEndpointRouteBuilderExtensions
{
    // public static IEndpointConventionBuilder MapOpenApi(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern = OpenApiConstants.DefaultOpenApiRoute, OpenApiFormat format = OpenApiFormat.Json)
    // {
    //     var options = endpoints.ServiceProvider.GetRequiredService<IOptionsMonitor<OpenApiOptions>>();
    //     return endpoints.MapGet(pattern, async (HttpContext context, string documentName = OpenApiConstants.DefaultDocumentName) =>
    //         {
    //             var documentService = context.RequestServices.GetKeyedService<OpenApiDocumentService>(documentName);
    //             if (documentService is null)
    //             {
    //                 context.Response.StatusCode = StatusCodes.Status404NotFound;
    //                 context.Response.ContentType = "text/plain;charset=utf-8";
    //                 await context.Response.WriteAsync($"No OpenAPI document with the name '{documentName}' was found.");
    //             }
    //             else
    //             {
    //                 var document = await documentService.GetOpenApiDocumentAsync(context.RequestServices, context.RequestAborted);
    //                 var documentOptions = options.Get(documentName);
    //                 using var output = MemoryBufferWriter.Get();
    //                 using var writer = Utf8BufferTextWriter.Get(output);
    //                 try
    //                 {
    //                     switch (format)
    //                     {
    //                         case OpenApiFormat.Json:
    //                             document.Serialize(new OpenApiJsonWriter(writer), documentOptions.OpenApiVersion);
    //                             context.Response.ContentType = "application/json;charset=utf-8";
    //                             await context.Response.BodyWriter.WriteAsync(output.ToArray(), context.RequestAborted);
    //                             await context.Response.BodyWriter.FlushAsync(context.RequestAborted);
    //                             break;
    //                         case OpenApiFormat.Yaml:
    //                             document.Serialize(new OpenApiYamlWriter(writer), documentOptions.OpenApiVersion);
    //                             context.Response.ContentType = "text/yaml;charset=utf-8";
    //                             await context.Response.BodyWriter.WriteAsync(output.ToArray(), context.RequestAborted);
    //                             await context.Response.BodyWriter.FlushAsync(context.RequestAborted);
    //                             break;
    //                         default:
    //                             throw new ArgumentOutOfRangeException(nameof(format), format, null);
    //                     }
    //                 }
    //                 finally
    //                 {
    //                     MemoryBufferWriter.Return(output);
    //                     Utf8BufferTextWriter.Return(writer);
    //                 }
    //
    //             }
    //         }).ExcludeFromDescription();
    // }
}