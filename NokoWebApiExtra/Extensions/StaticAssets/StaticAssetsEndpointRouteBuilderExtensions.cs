namespace NokoWebApiExtra.Extensions.StaticAssets;

public static class StaticAssetsEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapStaticAssets(this IEndpointRouteBuilder endpoints, string path = "public")
    {
        endpoints.MapGet(path, async context =>
        {
            var ctxReqPathValue = context.Request.Path.Value?.TrimStart('/') ?? "index.html";
            var filePath = Path.Combine(path, ctxReqPathValue);
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
