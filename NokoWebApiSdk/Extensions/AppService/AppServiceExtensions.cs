using System.Reflection;
using NokoWebApiSdk.Annotations;

namespace NokoWebApiSdk.Extensions.AppService;

public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Assemblies In Current App Domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        
        // Get Executing Assembly In Internal Assembly
        var executingAssembly = Assembly.GetExecutingAssembly();
        if (!assemblies.Contains(executingAssembly))
        {
            assemblies.Add(executingAssembly);
        }
        
        // Get Type Of App Service Configurable And Initialized 
        var appServiceInitializedType = typeof(IAppServiceInitialized);

        // Get Types From Assemblies And Filtering With App Service Attribute
        var appServiceTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
            {
                var attribute = t.GetCustomAttribute<AppServiceAttribute>();
                var isAssignable = appServiceInitializedType.IsAssignableFrom(t);
                return attribute is not null && isAssignable && t is { IsClass: true, IsPublic: true };
            });

        foreach (var appServiceType in appServiceTypes)
        {
            if (appServiceType.GetConstructor([]) is null)
            {
                throw new Exception("Cannot create a AppService from a class that doesn't have a parameterless constructor.");
            }

            // Create New Instance From App Service Type 
            var appService = Activator.CreateInstance(appServiceType)!;
            
            // Check App Service Is Assignable From App Service Initialized Type
            var isAssignableAppServiceInitialized = appServiceInitializedType.IsAssignableFrom(appServiceType);
            if (isAssignableAppServiceInitialized && appService is IAppServiceInitialized appServiceInitialized)
            {
                
                // Initialized App Service With Service Collections
                appServiceInitialized.OnInitialized(services, configuration);
                
                // Add Singleton To Services
                services.AddSingleton(appServiceInitializedType, appServiceInitialized);
            }
        }

        return services;
    }

    public static void UseAppServices(this WebApplication app, IWebHostEnvironment? env = null)
    {
        // Get App Services From App Service Collections
        var appServiceConfigurables = app.Services.GetServices<IAppServiceInitialized>();
        
        // Get Web Host Environment
        env ??= app.Environment;
        
        foreach (var appServiceConfigurable in appServiceConfigurables)
        {
            // Configuring App Service With Web Application Context And Web Host Environment
            appServiceConfigurable.OnConfigure(app, env);
        }
    }
}
