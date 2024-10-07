using System.Reflection;

namespace NokoWebApiExtra.Extensions.ApiService;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Get Assemblies In Current App Domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        
        // Get Executing Assembly In Internal Assembly
        var executingAssembly = Assembly.GetExecutingAssembly();
        if (!assemblies.Contains(executingAssembly))
        {
            assemblies.Add(executingAssembly);
        }
        
        // Get Type Of Api Service Configurable And Initialized 
        var apiServiceConfigureInitialized = typeof(IApiServiceConfigureInitialized);
        var apiServiceConfigurableType = typeof(IApiServiceConfigurable);
        var apiServiceInitializedType = typeof(IApiServiceInitialized);

        // Get Types From Assemblies And Filtering With Api Service Attribute
        var apiServiceTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
            {
                var attribute = t.GetCustomAttribute<ApiServiceAttribute>();
                var isAssignable = apiServiceConfigureInitialized.IsAssignableFrom(t)
                                   || apiServiceConfigurableType.IsAssignableFrom(t) 
                                   || apiServiceInitializedType.IsAssignableFrom(t);
                return attribute is not null && isAssignable && t is { IsClass: true, IsPublic: true };
            });

        foreach (var apiServiceType in apiServiceTypes)
        {
            // Create New Instance From Api Service Type 
            var apiService = Activator.CreateInstance(apiServiceType)!;
            
            // Check Api Service Is Assignable From Api Service Configure Initialized Type
            var isAssignableApiServiceConfigureInitialized = typeof(IApiServiceConfigureInitialized).IsAssignableFrom(apiServiceType);
            
            // Check Api Service Is Assignable From Api Service Initialized Type
            var isAssignableApiServiceInitialized = isAssignableApiServiceConfigureInitialized || apiServiceInitializedType.IsAssignableFrom(apiServiceType);
            if (isAssignableApiServiceInitialized && apiService is IApiServiceInitialized apiServiceInitialized)
            {
                
                // Initialized Api Service With Service Collections
                apiServiceInitialized.OnInitialized(services);
            }

            // Check Api Service Is Assignable From Api Service Configurable Type
            var isAssignableApiServiceConfigurable = isAssignableApiServiceConfigureInitialized || apiServiceConfigurableType.IsAssignableFrom(apiServiceType);
            if (isAssignableApiServiceConfigurable && apiService is IApiServiceConfigurable apiServiceConfigurable)
            {
                // Add Singleton To Services
                services.AddSingleton(apiServiceConfigurableType, apiServiceConfigurable);
            }
        }

        return services;
    }

    public static void UseApiServices(this WebApplication app)
    {
        // Get Api Services From App Service Collections
        var apiServiceConfigurables = app.Services.GetServices<IApiServiceConfigurable>();
        
        // Get Web Host Environment
        var env = app.Environment;
        
        foreach (var apiServiceConfigurable in apiServiceConfigurables)
        {
            // Configuring Api Service With Web Application Context And Web Host Environment
            apiServiceConfigurable.OnConfigure(app, env);
        }
    }
}
