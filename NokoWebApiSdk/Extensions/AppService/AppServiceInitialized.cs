namespace NokoWebApiSdk.Extensions.AppService;

public abstract class AppServiceInitialized : IAppServiceInitialized
{
    public virtual void OnInitialized(IServiceCollection services, IConfiguration configuration) 
    {
        
    }
    
    public virtual void OnConfigure(WebApplication application, IWebHostEnvironment environment) 
    {
        
    }
}