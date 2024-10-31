using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ApiRepository;

public static class ApiRepositoryExtensions
{
    public static IServiceCollection AddApiRepositories(this IServiceCollection services, 
        Action<DbContextOptionsBuilder>? optionsAction = null, 
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped, 
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
    {
        // Get Assemblies In Current App Domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        
        // Get Executing Assembly In Internal Assembly
        var executingAssembly = Assembly.GetExecutingAssembly();
        if (!assemblies.Contains(executingAssembly))
        {
            assemblies.Add(executingAssembly);
        }
        
        // Get Type Of Db Context, Service Collection, Action Db Context Options Builder, And Service Lifetime
        var dbContextType = typeof(DbContext);
        var serviceCollectionType = typeof(IServiceCollection);
        var actionDbContextOptionsBuilderType = typeof(Action<DbContextOptionsBuilder>);
        var serviceLifetimeType = typeof(ServiceLifetime);
        
        // Get Type of Entity Framework Service Collection Extensions
        var entityFrameworkServiceCollectionExtensionsType = typeof(EntityFrameworkServiceCollectionExtensions);

        // Get Types From Assemblies And Filtering With Api Repository Attribute
        var baseRepositoryTypes = assemblies
            .SelectMany((assembly) => assembly.GetTypes())
            .Where((type) =>
            {
                var isAssignable = dbContextType.IsAssignableFrom(type);
                var hasAttribute = NokoCommonMod.HasAttribute<ApiRepositoryAttribute>(type);
                return type is { IsClass: true, IsPublic: true } && hasAttribute && isAssignable;
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
            var bRepositoryInstance = (DbContext)Activator.CreateInstance(baseRepositoryType, dbContextOptionsBuilder.Options)!;
            var bRepositoryInstanceType = bRepositoryInstance.GetType();

            var gData = new Dictionary<Type, Type>
            {
                [dbContextType] = bRepositoryInstanceType,
            };
            
            // var pTypes = new[] {
            //     serviceCollectionType, 
            //     actionDbContextOptionsBuilderType, 
            //     serviceLifetimeType, 
            //     serviceLifetimeType,
            // };
            
            var pTypes = new[] {
                serviceCollectionType, 
                actionDbContextOptionsBuilderType, 
                typeof(int),
            };

            var nokoWebReflectionHelper = new NokoReflectionHelper(entityFrameworkServiceCollectionExtensionsType);
            var dbContextMethod = nokoWebReflectionHelper.GetMethod("AddDbContextPool", gData, pTypes);
            if (dbContextMethod is null || !dbContextMethod.IsStatic) throw new Exception("The provided method must be static and not null.");
            
            // Call AddDbContent with Parameter Values
            // dbContextMethod.Invoke(null, [services, optionsAction, contextLifetime, optionsLifetime]);
            dbContextMethod.Invoke(null, [services, optionsAction, 1024]);
        }
        
        return services;
    }
}