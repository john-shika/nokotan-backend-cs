using NokoWebApiSdk.Middlewares;

namespace NokoWebApiSdk.Extensions.CustomException;

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}