namespace NokoWebApiSdk.Extensions.ConfigurationBinder;

public static class ConfigurationBinderExtensions
{
    public static T GetConfig<T>(this IConfiguration configuration)
        where T : class, new()
    {
        return configuration.GetConfig<T>(nameof(T));
    }
    
    public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
        where T : class, new()
    {
        var settings = new T(); 
        configuration.GetSection(sectionName).Bind(settings); 
        return settings;
    }
}
