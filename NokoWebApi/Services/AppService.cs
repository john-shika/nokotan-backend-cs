using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Filters;
using NokoWebApiSdk.Json.Converters;

namespace NokoWebApi.Services;

[AppService]
public class AppService : AppServiceInitialized
{
    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        // services.AddEndpointsApiExplorer();
        services.AddAntiforgery();
        
        // testing using NokoWebApi.Optimizes.EntryPoint with AspNetCore Minimal Api
        // services.AddControllers((options) =>
        // {
        //     options.Filters.Add<CustomValidationFilter>();
        //     options.Filters.Add<HttpExceptionFilter>();
        // });
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        services.AddHsts((options) =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365);
        });
    }
    
    public override void OnConfigure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            app.UseAntiforgery();
            app.UseHsts();
        }

        // app.UseCustomExceptionMiddleware();

        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseStaticFiles();
        
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllerRoute(
        //         name: "default",
        //         pattern: "{controller=Home}/{action=Index}/{id?}");
        // });

        app.UseEndpoints((endpoints) =>
        {
            // testing using NokoWebApi.Optimizes.EntryPoint with AspNetCore Minimal Api
            // endpoints.MapControllers();
        });

        // app.MapControllers();
        // app.MapStaticAssets();
    }
}