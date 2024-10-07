using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace NokoWebApiExtra.Extensions.ApiRepository;

public static class ApiRepositoryExtensions
{
    public static IServiceCollection AddApiRepositories(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
    {
        // Get Assemblies In Current App Domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        
        // Get Executing Assembly In Internal Assembly
        var executingAssembly = Assembly.GetExecutingAssembly();
        if (!assemblies.Contains(executingAssembly))
        {
            assemblies.Add(executingAssembly);
        }
        
        // Get Type Of Base Repository
        var dbContextType = typeof(DbContext);
        
        // Get Type of Entity Framework Service Collection Extensions
        var entityFrameworkServiceCollectionExtensionsType = typeof(EntityFrameworkServiceCollectionExtensions);

        // Get Types From Assemblies And Filtering With Api Repository Attribute
        var baseRepositoryTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
            {
                var attribute = t.GetCustomAttribute<ApiRepositoryAttribute>();
                var isAssignable = dbContextType.IsAssignableFrom(t);
                return attribute is not null && isAssignable && t is { IsClass: true, IsPublic: true };
            });

        foreach (var baseRepositoryType in baseRepositoryTypes)
        {
            // Create DbContextOptions
            var dbContextOptionsBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(baseRepositoryType);
            var dbContextOptionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(dbContextOptionsBuilderType)!;
            optionsAction?.Invoke(dbContextOptionsBuilder);

            // Check Base Repository Have Constructor With Parameter Database Context Options
            var dbContextOptionsType = dbContextOptionsBuilder.Options.GetType();
            if (baseRepositoryType.GetConstructor([dbContextOptionsType]) is null)
            {
                throw new Exception("Cannot create a BaseRepository from a class that doesn't have a single parameter DbContextOptions constructor.");
            }

            // Create Base Repository New Instance
            var baseRepositoryInstance = (DbContext)Activator.CreateInstance(baseRepositoryType, dbContextOptionsBuilder.Options)!;
            var baseRepositoryInstanceType = baseRepositoryInstance.GetType();
            
            // Services Add Database Context With Options Action, Context Lifetime, And Options Lifetime
            // var serviceCollectionType = typeof(IServiceCollection);
            // var actionDbContextOptionsBuilderType = typeof(Action<DbContextOptionsBuilder>);
            // var serviceLifetimeType = typeof(ServiceLifetime);
            // Type[] types = [
            //     serviceCollectionType, 
            //     actionDbContextOptionsBuilderType, 
            //     serviceLifetimeType, 
            //     serviceLifetimeType,
            // ];
            // var method = services.GetType().GetMethod("AddDbContext", types)!;
            // var genericMethod = method.MakeGenericMethod(baseRepositoryInstanceType);
            // genericMethod.Invoke(null, [services, optionsAction, contextLifetime, optionsLifetime]);
            
            var method = entityFrameworkServiceCollectionExtensionsType
                .GetMethods()
                .FirstOrDefault(m =>
                    m is { Name: "AddDbContext", IsGenericMethodDefinition: true }
                    && m.GetGenericArguments().Length == 1
                    && m.GetParameters().Length == 4)!;
            var genericMethod = method.MakeGenericMethod(baseRepositoryInstanceType);
            genericMethod.Invoke(null, [services, optionsAction, contextLifetime, optionsLifetime]);
        }
        
        return services;
    }
}