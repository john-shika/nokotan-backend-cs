namespace NokoWebApiSdk.Extensions.MapStaticAssets;

public static class MapStaticAssetsEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapStaticAssets(this IEndpointRouteBuilder endpoints, string path = "public")
    {
        endpoints.MapGet(path, async context =>
        {
            // TODO: Fix Path Traversal Vulnerabilities.
            var ctxReqPathValue = context.Request.Path.Value?.TrimStart('/') ?? "index.html";
            var filePath = Path.Combine(path, ctxReqPathValue);
            var fullPath = Path.GetFullPath(filePath);
            var basePath = Path.GetFullPath(path);

            if (!fullPath.StartsWith(basePath))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access Denied.");
                return;
            }

            if (File.Exists(filePath))
            {
                await context.Response.SendFileAsync(filePath);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        });

        return endpoints;
    }
}
