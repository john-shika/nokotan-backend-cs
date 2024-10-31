using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Extensions.ConfigurationBinder;

public static class ConfigurationBinderExtensions
{
    public static T GetConfig<T>(this IConfiguration configuration)
        where T : class, new()
    {
        var name = typeof(T).Name;
        return configuration.GetConfig<T>(name);
    }
    
    public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
        where T : class, new()
    {
        var settings = new T();
        configuration.GetSection(sectionName).Bind(settings); 
        return settings;
    }
}
