using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Extensions.ConfigurationBinder;
using NokoWebApiSdk.Globals;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Extensions.NokoWebApi;

public static class NokoWebApplicationExtensions
{
    // public static IServiceCollection AddNokoWebApi(this IServiceCollection services)
    // {
    //     return services;
    // }
    
    // public static void UseNokoWebApi(this WebApplication app)
    // {
    // }

    public static void UseGlobals(this NokoWebApplication application, IConfiguration? configuration = null)
    {
        application.Listen((nokoWebApplication) =>
        {
            configuration ??= nokoWebApplication.Configuration;
            if (configuration is null) return;
        
            var nokoWebApplicationDefaults = new NokoWebApplicationGlobals(configuration);
            nokoWebApplication.Globals = nokoWebApplicationDefaults;
        });
    }
}