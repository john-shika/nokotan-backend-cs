namespace NokoWebApiSdk.Extensions.ApiModule;

public interface IApiModuleInitialized
{
    public void OnInitialized(IServiceCollection services, IConfiguration configuration);
}