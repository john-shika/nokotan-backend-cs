using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Filters;
using NokoWebApiSdk.Json.Converters;

namespace NokoWebApiSdk.Json.Services;

[AppService]
public class JsonService : AppServiceInitialized
{
    public static void JsonSerializerConfigure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        
        var converters = options.Converters;
        converters.Add(new JsonDateTimeConverter());
    }

    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JsonOptions>((options) =>
        {
            JsonSerializerConfigure(options.JsonSerializerOptions);
        });
    }
    
    public override void OnConfigure(WebApplication app, IWebHostEnvironment env)
    {
        // do nothing...
    }
}