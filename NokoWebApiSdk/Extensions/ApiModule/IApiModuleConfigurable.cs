namespace NokoWebApiSdk.Extensions.ApiModule;

public interface IApiModuleConfigurable
{
    public void OnConfigure(WebApplication app, IWebHostEnvironment env);
}