using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.AppService;

public static class AppServiceExtensions
{
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "The member is accessed dynamically.")]
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
            .SelectMany((assembly) => assembly.GetTypes())
            .Where((type) =>
            {
                var isAssignable = appServiceInitializedType.IsAssignableFrom(type);
                var hasAttribute = NokoCommonMod.HasAttribute<AppServiceAttribute>(type);
                return type is { IsClass: true, IsPublic: true } && hasAttribute && isAssignable;
            });

        foreach (var appServiceType in appServiceTypes)
        {
            if (appServiceType.GetConstructor([]) is null)
            {
                throw new Exception("Cannot create a AppService from a class that doesn't have a parameterless constructor.");
            }

            // Create New Instance From App Service Type 
            var appService = Activator.CreateInstance(appServiceType)!;
            
            // Get appServiceInitialized By Checking with Interface
            if (appService is not IAppServiceInitialized appServiceInitialized) continue;
            
            // Initialized App Service With Service Collections
            appServiceInitialized.OnInitialized(services, configuration);
                
            // Add Singleton To Services
            services.AddSingleton(appServiceInitializedType, appServiceInitialized);
        }

        return services;
    }

    public static void UseAppServices(this WebApplication application, IWebHostEnvironment? env = null)
    {
        // Get App Services From App Service Collections
        var appServices = application.Services.GetServices<IAppServiceInitialized>();
        
        // Get Web Host Environment
        env ??= application.Environment;
        
        foreach (var appService in appServices)
        {
            // Configuring App Service With Web Application Context And Web Host Environment
            appService.OnConfigure(application, env);
        }
    }
}
