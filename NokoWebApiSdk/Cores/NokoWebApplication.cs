using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NokoWebApiSdk.Extensions.ApiRepository;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Extensions.NokoWebApi;
using NokoWebApiSdk.Extensions.ScalarOpenApi;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;
using NokoWebApiSdk.Globals;

namespace NokoWebApiSdk.Cores;

public interface INokoWebApplication
{
    public WebApplicationBuilder Builder { get; init; }
    public IServiceCollection Services => Builder.Services;
    public WebApplication? Application { get; set; }
    public IWebHostEnvironment? Environment => Application?.Environment;
    public IHostApplicationLifetime? Lifetime => Application?.Lifetime;
    public ILogger? Logger => Application?.Logger;
    public IList<NokoWebApplicationListenerDelegate?> Listeners { get; }
    public IServiceCollection Repository(Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped);
    public WebApplication? MapOpenApi(Action<ScalarOpenApiOptions>? configureOptions = null);
    public WebApplication Build();
    public void Run();
    public Task RunAsync([StringSyntax("Uri")] string? url = null);
    public void Start();
    public Task StartAsync(CancellationToken cancellationToken = default);
}

public class NokoWebApplication : INokoWebApplication
{
    public WebApplicationBuilder Builder { get; init; }
    public IServiceCollection Services => Builder.Services;
    public WebApplication? Application { get; set; }
    
    public NokoWebApplicationDefaults? Globals { get; set; }
    
    public IConfiguration? Configuration => Application?.Configuration;
    public IWebHostEnvironment? Environment => Application?.Environment;
    public IHostApplicationLifetime? Lifetime => Application?.Lifetime;
    public ILogger? Logger => Application?.Logger;

    public IList<NokoWebApplicationListenerDelegate?> Listeners { get; }
    
    public NokoWebApplication(string[] args)
    {
        Builder = WebApplication.CreateBuilder(args);
        Builder.Services.AddAppServices(Builder.Configuration);
        // some codes have been moved in services folder
        Listeners = [];
    }

    public void Listen(NokoWebApplicationListenerDelegate listener)
    {
        if (Application is null || Environment is null)
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

    public WebApplication? MapOpenApi(Action<ScalarOpenApiOptions>? configureOptions = null)
    {
        var action = (NokoWebApplication noko) =>
        {
            var app = noko.Application!;
            var env = noko.Environment!;
            
            var options = new ScalarOpenApiOptions();
            configureOptions?.Invoke(options);
        
            // TODO: some codes have been moved in services folder
            app.MapOpenApi(pattern: options.OpenApiRoutePattern)
                .RequireAuthorization()
                .AllowAnonymous();

            if (!env.IsDevelopment()) return;
        
            if (configureOptions is not null)
            {
                app.MapScalarOpenApiReference((scalarOptions) =>
                {
                    scalarOptions.CopyFrom(options);
                });
            }
            else
            {
                app.MapScalarApiReference();
            }
        
            app.UseDeveloperExceptionPage();
        };

        Listen(action.Listener);

        return Application;
    }

    public WebApplication Build()
    {
        if (Application is not null) return Application;
        Application = Builder.Build();
        Application.UseAppServices(Environment);
        return Application;
    }

    public void DetachEvent()
    {
        Build();
        // this.UseGlobals();
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
        DetachEvent();
        Application!.Run();
    }
    
    public async Task RunAsync([StringSyntax("Uri")] string? url = null)
    {
        DetachEvent();
        await Application!.RunAsync(url);
    }
    
    public void Start()
    {
        DetachEvent();
        Application!.Start();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        DetachEvent();
        await Application!.StartAsync(cancellationToken);
    }
}