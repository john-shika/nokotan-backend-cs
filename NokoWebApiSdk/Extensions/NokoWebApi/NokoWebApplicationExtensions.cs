using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Globals;

namespace NokoWebApiSdk.Extensions.NokoWebApi;

public static class NokoWebApplicationExtensions
{
    public static IServiceCollection AddNokoWebApi(this IServiceCollection services)
    {
        return services;
    }
    
    public static void UseNokoWebApi(this WebApplication app)
    {
    }

    public static void UseGlobals(this NokoWebApplication app, IConfiguration? config = null)
    {
        app.Listen((nokoWebApplication) =>
        {
            config ??= nokoWebApplication.Configuration;
            if (config is null) return;
        
            var nokoWebApplicationDefaults = new NokoWebApplicationDefaults(config);
            nokoWebApplication.Globals = nokoWebApplicationDefaults;
        });
    }
}