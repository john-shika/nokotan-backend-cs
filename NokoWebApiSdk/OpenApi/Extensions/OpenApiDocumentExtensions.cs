using Microsoft.OpenApi.Models;

namespace NokoWebApiSdk.OpenApi.Extensions;

public static class OpenApiDocumentExtensions
{
    public static OpenApiDocument Preload(this OpenApiDocument document)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.Parameters ??= new Dictionary<string, OpenApiParameter>();
        document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();
        document.Paths ??= new OpenApiPaths();
        document.SecurityRequirements ??= new List<OpenApiSecurityRequirement>();
        return document;
    }
}