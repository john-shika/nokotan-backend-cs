namespace NokoWebApiExtra.Extensions.ApiService;

public interface IApiServiceConfigurable
{
    public void OnConfigure(WebApplication app, IWebHostEnvironment env);
}