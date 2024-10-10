namespace NokoWebApiSdk.Extensions.OpenApi.Swagger;

public static class OpenApiSwaggerEndpointRouteBuilderExtensions
{
    // TODO: add Action<OpenApiSwaggerOptions> and connect with OpenApiScalarOptions CopyFrom both of them
    /// <summary>
    ///  Helper method to render Swagger UI view for testing.
    /// </summary>
    public static IEndpointConventionBuilder MapSwaggerApiReference(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/swagger/{documentName}", (string documentName) =>
        {
            // TODO: move all assets from cdn into local directory
            var config = new
            {
                ContentType = "text/html",
                DocumentName = documentName,
                DocumentUrl = $"/openapi/{documentName}.json",
                Layout = "StandaloneLayout",
            };
            return Results.Content($$"""
                <html>
                <head>
                    <meta charset="UTF-8">
                    <title>OpenAPI -- {{config.DocumentName}}</title>
                    <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css">
                </head>
                <body>
                    <div id="swagger-ui"></div>
                
                    <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js"></script>
                    <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js"></script>
                
                    <script>
                      function main() {
                        const ui = SwaggerUIBundle({
                        url: "{{config.DocumentUrl}}",
                          dom_id: '#swagger-ui',
                          deepLinking: true,
                          presets: [
                            SwaggerUIBundle.presets.apis,
                            SwaggerUIStandalonePreset,
                          ],
                          plugins: [
                            SwaggerUIBundle.plugins.DownloadUrl
                          ],
                          layout: "{{config.Layout}}",
                        })
                        window.ui = ui
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
                </body>
                </html>
                """, config.ContentType);
        }).ExcludeFromDescription();
    }
}