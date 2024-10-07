using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using NokoWebApiExtra.Extensions.Scalar.Options;
using NokoWebApiExtra.Extensions.Scalar.Mapper;

namespace NokoWebApiExtra.Extensions.Scalar;

/// <summary>
/// Extension methods for <see cref="IEndpointRouteBuilder" /> to provide required endpoints.
/// </summary>
public static class ScalarEndpointRouteBuilderExtensions
{
    private const string DocumentName = "{documentName}";

    /// <summary>
    /// Maps the Scalar API reference endpoint.
    /// </summary>
    /// <param name="endpoints"><see cref="IEndpointRouteBuilder" />.</param>
    public static IEndpointConventionBuilder MapScalarApiReference(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapScalarApiReference(_ =>{});
    }

    /// <summary>
    /// Maps the Scalar API reference endpoint.
    /// </summary>
    /// <param name="endpoints"><see cref="IEndpointRouteBuilder" />.</param>
    /// <param name="configureOptions">An action to configure the Scalar options.</param>
    public static IEndpointConventionBuilder MapScalarApiReference(this IEndpointRouteBuilder endpoints, Action<ScalarOptions> configureOptions)
    {
        var options = endpoints.ServiceProvider.GetService<IOptions<ScalarOptions>>()?.Value ?? new ScalarOptions();
        configureOptions(options);

        if (!options.EndpointPathPrefix.Contains(DocumentName))
        {
            throw new ArgumentException($"'EndpointPathPrefix' must define '{DocumentName}'.");
        }
        var configuration = JsonSerializer.Serialize(options.ToScalarConfiguration(), new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        });
        
        var configurationBytes = Encoding.UTF8.GetBytes(configuration);
        var configurationBytesJson = Convert.ToBase64String(configurationBytes);

        return endpoints.MapGet(options.EndpointPathPrefix, (string documentName) =>
            {
                var title = options.Title.Replace(DocumentName, documentName);
                var documentUrl = options.OpenApiRoutePattern.Replace(DocumentName, documentName);

                return Results.Content($$"""
                      <!doctype html>
                      <html lang="en">
                          <head>
                              <meta charset="utf-8" />
                              <meta name="viewport" content="width=device-width, initial-scale=1" />
                              <title>{{title}}</title>
                              <link rel="icon" type="image/x-icon" href="{{options.Favicon}}" />
                          </head>
                          <body>
                              <script id="api-reference" data-url="{{documentUrl}}"></script>
                              <script>
                                  function main() {
                                    const reference = document.getElementById('api-reference');
                                    if (typeof reference?.['dataset'] === 'object') {
                                      reference['dataset'].configuration = atob('{{configurationBytesJson}}');
                                    }
                                  }
                                  
                                  let called = false;
                                  function preload() {
                                    if (!called && document.readyState === 'complete') {
                                      called = true;
                                      main();
                                    }
                                  }
                                  
                                  window.addEventListener('load', preload);
                                  document.addEventListener('load', preload);
                                  document.addEventListener('DOMContentLoaded', preload);
                              </script>
                              <script src="{{options.CdnUrl}}"></script>
                          </body>
                      </html>
                      """, "text/html");
            })
            .ExcludeFromDescription();
    }
}