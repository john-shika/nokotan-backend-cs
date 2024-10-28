using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Filters;
using NokoWebApiSdk.Json.Converters;

namespace NokoWebApiSdk.Json.Services;

[AppService]
public class JsonSerializerService : AppServiceInitialized
{
    public static void Apply(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        
        var converters = options.Converters;
        converters.Add(new JsonDateTimeConverter());
    }

    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JsonOptions>((options) =>
        {
            Apply(options.JsonSerializerOptions);
        });
    }
}