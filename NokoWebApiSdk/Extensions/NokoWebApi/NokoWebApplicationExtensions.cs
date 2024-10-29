using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Globals;

namespace NokoWebApiSdk.Extensions.NokoWebApi;

public static class NokoWebApplicationExtensions
{
    public static IServiceCollection AddNokoWebApi(this IServiceCollection services)
    {
        // do nothing...
        return services;
    }
    
    public static void UseNokoWebApi(this WebApplication app)
    {
        // do nothing...
    }

    public static void UseGlobals(this NokoWebApplication application, IConfiguration? config = null)
    {
        application.Listen((nokoWebApplication) =>
        {
            config ??= nokoWebApplication.Configuration;
            if (config is null) return;
        
            var nokoWebApplicationDefaults = new NokoWebApplicationGlobals(config);
            nokoWebApplication.Globals = nokoWebApplicationDefaults;
        });
    }
}