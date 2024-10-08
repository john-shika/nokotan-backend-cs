using System.Reflection;
using NokoWebApiSdk.Annotations;

namespace NokoWebApiSdk.Extensions.ApiModule;

public static class ApiModuleExtensions
{
    public static IServiceCollection AddApiModules(this IServiceCollection services)
    {
        // Get Assemblies In Current App Domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        
        // Get Executing Assembly In Internal Assembly
        var executingAssembly = Assembly.GetExecutingAssembly();
        if (!assemblies.Contains(executingAssembly))
        {
            assemblies.Add(executingAssembly);
        }
        
        // Get Type Of Api Module Configurable And Initialized 
        var apiModuleConfigureInitialized = typeof(IApiModuleConfigureInitialized);
        var apiModuleConfigurableType = typeof(IApiModuleConfigurable);
        var apiModuleInitializedType = typeof(IApiModuleInitialized);

        // Get Types From Assemblies And Filtering With Api Module Attribute
        var apiModuleTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
            {
                var attribute = t.GetCustomAttribute<ApiModuleAttribute>();
                var isAssignable = apiModuleConfigureInitialized.IsAssignableFrom(t)
                                   || apiModuleConfigurableType.IsAssignableFrom(t) 
                                   || apiModuleInitializedType.IsAssignableFrom(t);
                return attribute is not null && isAssignable && t is { IsClass: true, IsPublic: true };
            });

        foreach (var apiModuleType in apiModuleTypes)
        {
            if (apiModuleType.GetConstructor([]) is null)
            {
                throw new Exception("Cannot create a ApiModule from a class that doesn't have a parameterless constructor.");
            }

            // Create New Instance From Api Module Type 
            var apiModule = Activator.CreateInstance(apiModuleType)!;
            
            // Check Api Service Is Assignable From Api Module Configure Initialized Type
            var isAssignableApiModuleConfigureInitialized = typeof(IApiModuleConfigureInitialized).IsAssignableFrom(apiModuleType);
            
            // Check Api Service Is Assignable From Api Module Initialized Type
            var isAssignableApiModuleInitialized = isAssignableApiModuleConfigureInitialized || apiModuleInitializedType.IsAssignableFrom(apiModuleType);
            if (isAssignableApiModuleInitialized && apiModule is IApiModuleInitialized apiModuleInitialized)
            {
                
                // Initialized Api Module With Service Collections
                apiModuleInitialized.OnInitialized(services);
            }

            // Check Api Service Is Assignable From Api Module Configurable Type
            var isAssignableApiModuleConfigurable = isAssignableApiModuleConfigureInitialized || apiModuleConfigurableType.IsAssignableFrom(apiModuleType);
            if (isAssignableApiModuleConfigurable && apiModule is IApiModuleConfigurable apiModuleConfigurable)
            {
                // Add Singleton To Services
                services.AddSingleton(apiModuleConfigurableType, apiModuleConfigurable);
            }
        }

        return services;
    }

    public static void UseApiModules(this WebApplication app, IWebHostEnvironment? env = null)
    {
        // Get Api Modules From App Service Collections
        var apiModuleConfigurables = app.Services.GetServices<IApiModuleConfigurable>();
        
        // Get Web Host Environment
        env ??= app.Environment;
        
        foreach (var apiModuleConfigurable in apiModuleConfigurables)
        {
            // Configuring Api Module With Web Application Context And Web Host Environment
            apiModuleConfigurable.OnConfigure(app, env);
        }
    }
}
