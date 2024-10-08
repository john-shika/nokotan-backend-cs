using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NokoWebApiSdk.Exceptions;
using NokoWebApiSdk.Extensions.ApiModule;
using NokoWebApiSdk.Extensions.ApiRepository;
using NokoWebApiSdk.Extensions.Scalar;
using NokoWebApiSdk.Extensions.Scalar.Options;

namespace NokoWebApiSdk;

public interface INokoWebApplication
{
    public WebApplicationBuilder WebAppBuilder { get; init; }
    public IServiceCollection Services => WebAppBuilder.Services;
    public WebApplication? WebApp { get; set; }
    public IWebHostEnvironment? WebHostEnv => WebApp?.Environment;
    public IHostApplicationLifetime? AppLifetime => WebApp?.Lifetime;
    public ILogger? Logger => WebApp?.Logger;
    public IServiceCollection Repository(Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped);
    public WebApplication? MapOpenApi(Action<ScalarOptions>? configureOptions = null);
    public WebApplication Build();
    public void Run();
    public Task RunAsync([StringSyntax("Uri")] string? url = null);
    public void Start();
    public Task StartAsync(CancellationToken cancellationToken = default);
}

public class NokoWebApplication : INokoWebApplication
{
    public WebApplicationBuilder WebAppBuilder { get; init; }
    public IServiceCollection Services => WebAppBuilder.Services;
    public WebApplication? WebApp { get; set; }
    
    public IWebHostEnvironment? WebHostEnv => WebApp?.Environment;
    public IHostApplicationLifetime? AppLifetime => WebApp?.Lifetime;
    public ILogger? Logger => WebApp?.Logger;

    public List<Action<NokoWebApplication>?> Listeners { get; }
    
    public NokoWebApplication(string[] args)
    {
        WebAppBuilder = WebApplication.CreateBuilder(args);
        WebAppBuilder.Services.AddApiModules(WebAppBuilder.Configuration);
        WebAppBuilder.Services.AddOpenApi();
        Listeners = [];
    }

    protected void InvokeOrListen(Action<NokoWebApplication> listener)
    {
        if (WebApp is null || WebHostEnv is null)
        {
            Listeners.Add(listener);
        }
        else
        {
            listener.Invoke(this);
        }
    }

    public static NokoWebApplication Create(string[] args)
    {
        return new NokoWebApplication(args);
    }

    public IServiceCollection Repository(Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
    {
        Services.AddApiRepositories(optionsAction, contextLifetime, optionsLifetime);
        return Services;
    }

    public WebApplication? MapOpenApi(Action<ScalarOptions>? configureOptions = null)
    {
        var listener = (NokoWebApplication noko) =>
        {
            var webApp = noko.WebApp!;
            var webHostEnv = noko.WebHostEnv!;
            
            var options = new ScalarOptions();
            configureOptions?.Invoke(options);
        
            webApp.MapOpenApi(pattern: options.OpenApiRoutePattern).AllowAnonymous();

            if (!webHostEnv.IsDevelopment()) return;
        
            if (configureOptions is not null)
            {
                webApp.MapScalarApiReference((scalarOptions) =>
                {
                    scalarOptions.CopyFrom(options);
                });
            }
            else
            {
                webApp.MapScalarApiReference();
            }
        
            webApp.UseDeveloperExceptionPage();
        };

        InvokeOrListen(listener);

        return WebApp;
    }

    public WebApplication Build()
    {
        if (WebApp is not null) return WebApp;
        WebApp = WebAppBuilder.Build();
        WebApp.UseApiModules(WebHostEnv);
        return WebApp;
    }

    public void BuildAndEmitListen()
    {
        Build();
        for (var i = 0; i < Listeners.Count; i++)
        {
            var listener = Listeners[i];
            if (listener is null) continue;
            listener.Invoke(this);
            Listeners[i] = null;
        }
    }

    public void Run()
    {
        BuildAndEmitListen();
        WebApp!.Run();
    }
    
    public async Task RunAsync([StringSyntax("Uri")] string? url = null)
    {
        BuildAndEmitListen();
        await WebApp!.RunAsync(url);
    }
    
    public void Start()
    {
        BuildAndEmitListen();
        WebApp!.Start();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        BuildAndEmitListen();
        await WebApp!.StartAsync(cancellationToken);
    }
}