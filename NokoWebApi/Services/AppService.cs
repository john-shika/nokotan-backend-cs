using NokoWebApiExtra.Extensions.ApiService;
using NokoWebApiExtra.Extensions.Scalar;
using NokoWebApiExtra.Extensions.Scalar.Enums;
using NokoWebApiExtra.Extensions.Scalar.Options;

namespace NokoWebApi.Services;

[ApiService]
public class AppService : IApiServiceConfigureInitialized
{
    public void OnInitialized(IServiceCollection services)
    {
        services.AddAntiforgery();
        services.AddControllers();
        services.AddOpenApi();
    }
    
    public void OnConfigure(WebApplication app, IWebHostEnvironment env)
    {
        app.MapOpenApi(pattern: "/openapi/{documentName}.json");

        if (env.IsDevelopment())
        {
            // Path: /scalar/v1
            app.MapScalarApiReference((ScalarOptions options) =>
            {
                options.Title = "Scalar API Reference -- {documentName}";
                options.EndpointPathPrefix = "/scalar/{documentName}";
                options.OpenApiRoutePattern = "/openapi/{documentName}.json";
                // options.CdnUrl = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";
                options.CdnUrl = "/js/scalar.api-reference.js";
                options.Theme = ScalarTheme.Purple;
                options.Favicon = "/favicon.ico";
            });

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseAuthorization();
        app.UseAntiforgery();
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