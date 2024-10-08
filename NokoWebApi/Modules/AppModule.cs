using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.ApiModule;
using NokoWebApiSdk.Extensions.Scalar;
using NokoWebApiSdk.Extensions.Scalar.Enums;
using NokoWebApiSdk.Extensions.Scalar.Options;

namespace NokoWebApi.Modules;

[ApiModule]
public class AppModule : IApiModuleConfigureInitialized
{
    public void OnInitialized(IServiceCollection services)
    {
        services.AddAntiforgery();
        services.AddControllers();
        services.AddHsts((options) =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365);
        });
    }
    
    public void OnConfigure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            app.UseAntiforgery();
            app.UseHsts();
        }

        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllerRoute(
        //         name: "default",
        //         pattern: "{controller=Home}/{action=Index}/{id?}");
        // });

        app.MapControllers();
        // app.MapStaticAssets();
    }
}