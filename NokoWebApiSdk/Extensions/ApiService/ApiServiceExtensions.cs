using System.Reflection;
using NokoWebApiSdk.Annotations;

namespace NokoWebApiSdk.Extensions.ApiService;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
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
        var apiServiceInitializedType = typeof(IApiServiceInitialized);

        // Get Types From Assemblies And Filtering With Api Service Attribute
        var apiServiceTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
            {
                var attribute = t.GetCustomAttribute<ApiServiceAttribute>();
                var isAssignable = apiServiceInitializedType.IsAssignableFrom(t);
                return attribute is not null && isAssignable && t is { IsClass: true, IsPublic: true };
            });

        foreach (var apiServiceType in apiServiceTypes)
        {
            if (apiServiceType.GetConstructor([]) is null)
            {
                throw new Exception("Cannot create a ApiService from a class that doesn't have a parameterless constructor.");
            }

            // Create New Instance From Api Service Type 
            var apiService = Activator.CreateInstance(apiServiceType)!;
            
            // Check Api Service Is Assignable From Api Service Initialized Type
            var isAssignableApiServiceInitialized = apiServiceInitializedType.IsAssignableFrom(apiServiceType);
            if (isAssignableApiServiceInitialized && apiService is IApiServiceInitialized apiServiceInitialized)
            {
                
                // Initialized Api Service With Service Collections
                apiServiceInitialized.OnInitialized(services, configuration);
                
                // Add Singleton To Services
                services.AddSingleton(apiServiceInitializedType, apiServiceInitialized);
            }
        }

        return services;
    }

    public static void UseApiServices(this WebApplication app, IWebHostEnvironment? env = null)
    {
        // Get Api Services From App Service Collections
        var apiServiceConfigurables = app.Services.GetServices<IApiServiceInitialized>();
        
        // Get Web Host Environment
        env ??= app.Environment;
        
        foreach (var apiServiceConfigurable in apiServiceConfigurables)
        {
            // Configuring Api Service With Web Application Context And Web Host Environment
            apiServiceConfigurable.OnConfigure(app, env);
        }
    }
}
