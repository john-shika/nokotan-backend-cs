namespace NokoWebApiSdk.Extensions.ApiService;

public interface IApiServiceInitialized
{
    public void OnInitialized(IServiceCollection services, IConfiguration configuration);
    public void OnConfigure(WebApplication application, IWebHostEnvironment environment);
}