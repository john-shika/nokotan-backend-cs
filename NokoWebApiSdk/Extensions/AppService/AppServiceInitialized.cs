namespace NokoWebApiSdk.Extensions.AppService;

public interface IAppServiceInitialized
{
    public void OnInitialized(IServiceCollection services, IConfiguration configuration);
    public void OnConfigure(WebApplication application, IWebHostEnvironment environment);
}

public abstract class AppServiceInitialized : IAppServiceInitialized
{
    public virtual void OnInitialized(IServiceCollection services, IConfiguration configuration) 
    {
        
    }
    
    public virtual void OnConfigure(WebApplication application, IWebHostEnvironment environment) 
    {
        
    }
}