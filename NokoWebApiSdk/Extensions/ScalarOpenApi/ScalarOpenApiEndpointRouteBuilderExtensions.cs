using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Mapper;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi;

/// <summary>
/// Extension methods for <see cref="IEndpointRouteBuilder" /> to provide required endpoints.
/// </summary>
public static class ScalarOpenApiEndpointRouteBuilderExtensions
{
    private const string DocumentName = "{documentName}";

    /// <summary>
    /// Maps the Scalar API reference endpoint.
    /// </summary>
    /// <param name="endpoints"><see cref="IEndpointRouteBuilder" />.</param>
    public static IEndpointConventionBuilder MapScalarApiReference(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapScalarOpenApiReference(_ =>{});
    }

    /// <summary>
    /// Maps the Scalar API reference endpoint.
    /// </summary>
    /// <param name="endpoints"><see cref="IEndpointRouteBuilder" />.</param>
    /// <param name="configureOptions">An action to configure the Scalar options.</param>
    public static IEndpointConventionBuilder MapScalarOpenApiReference(this IEndpointRouteBuilder endpoints, Action<ScalarOpenApiOptions> configureOptions)
    {
        var options = endpoints.ServiceProvider.GetService<IOptions<ScalarOpenApiOptions>>()?.Value ?? new ScalarOpenApiOptions();
        configureOptions(options);

        if (!options.EndpointPathPrefix.Contains(DocumentName))
        {
            throw new ArgumentException($"'EndpointPathPrefix' must define '{DocumentName}'.");
        }
        
        var config = JsonSerializer.Serialize(options.ToOpenApiScalarConfiguration(), new JsonSerializerOptions
        {
            Converters =
            {
                new ScalarOpenApiClientSerializeConverter(),
                new ScalarOpenApiTargetSerializeConverter(),
                new ScalarOpenApiThemeSerializeConverter(),
                new TagSorterSerializeConverter(),
                new ThemeModeSerializeConverter(),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        });
        
        var dataConfig = Convert.ToBase64String(Encoding.UTF8.GetBytes(config));

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
                              
                              <!-- distributed by options.CssBundlePathFile -->
                              <!-- <link rel="stylesheet" href="/css/scalar.api-reference.css" /> -->
                          </head>
                          <body>
                              <script id="api-reference" data-url="{{documentUrl}}"></script>
                              <script>
                                function main() {
                                  const data = atob('{{dataConfig}}');
                                
                                  // inject scalar api reference configuration, maybe failed
                                  const reference = document.getElementById('api-reference');
                                  if (typeof reference?.['dataset'] === 'object') {
                                    reference['dataset'].configuration = data;
                                  }
                                  
                                  // reload scalar api reference configuration
                                  const ev = new CustomEvent('scalar:update-references-config', {
                                    detail: {
                                      configuration: JSON.parse(data),
                                    },
                                  });
                                  document.dispatchEvent(ev);
                                  
                                  // set border radius to zero
                                  const styleElement = document.createElement('style');
                                  document.head.appendChild(styleElement);
                                  const cssStyleSheet = styleElement.sheet; 
                                  
                                  cssStyleSheet.addRule(':root', '--scalar-radius: 0px !important; --scalar-radius-lg: 0px !important; --scalar-radius-xl: 0px !important;');
                                  cssStyleSheet.addRule(':hover', 'border-radius: 0px !important;');
                                  cssStyleSheet.addRule(':focus', 'border-radius: 0px !important;');
                                  cssStyleSheet.addRule('::before', 'border-radius: 0px !important;');
                                  cssStyleSheet.addRule('::after', 'border-radius: 0px !important;');
                                  cssStyleSheet.addRule('select, optgroup, option', 'border-radius: 0px !important;');
                                }
                                
                                let called = false;
                                function preload() {
                                  const presets = ['loading', 'interactive', 'complete'];
                                  if (!called && presets.includes(document.readyState)) {
                                    called = true;
                                    main();
                                  }
                                }
                                
                                window.addEventListener('load', preload);
                                document.addEventListener('load', preload);
                                document.addEventListener('DOMContentLoaded', preload);
                              </script>
                              
                              <!-- distributed by options.JsBundlePathFile -->
                              <script src="{{options.CdnUrl}}"></script>
                          </body>
                      </html>
                      """, "text/html");
            })
            .ExcludeFromDescription();
    }
}