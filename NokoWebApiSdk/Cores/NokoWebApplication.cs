using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NokoWebApiSdk.Extensions.ApiRepository;
using NokoWebApiSdk.Extensions.ApiService;
using NokoWebApiSdk.Extensions.OpenApi.Scalar;
using NokoWebApiSdk.Extensions.OpenApi.Scalar.Options;
using NokoWebApiSdk.Extensions.OpenApi.Swagger;

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
    public WebApplication? MapOpenApi(Action<OpenApiScalarOptions>? configureOptions = null);
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
    
    public IWebHostEnvironment? Environment => Application?.Environment;
    public IHostApplicationLifetime? Lifetime => Application?.Lifetime;
    public ILogger? Logger => Application?.Logger;

    public IList<NokoWebApplicationListenerDelegate?> Listeners { get; }
    
    public NokoWebApplication(string[] args)
    {
        Builder = WebApplication.CreateBuilder(args);
        Builder.Services.AddApiServices(Builder.Configuration);
        // some codes have been moved in services folder
        Listeners = [];
    }

    protected void InvokeOrListen(NokoWebApplicationListenerDelegate listener)
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

    public WebApplication? MapOpenApi(Action<OpenApiScalarOptions>? configureOptions = null)
    {
        var action = (NokoWebApplication noko) =>
        {
            var app = noko.Application!;
            var env = noko.Environment!;
            
            var options = new OpenApiScalarOptions();
            configureOptions?.Invoke(options);
        
            // TODO: some codes have been moved in services folder
            app.MapOpenApi(pattern: options.OpenApiRoutePattern)
                .RequireAuthorization()
                .AllowAnonymous();

            if (!env.IsDevelopment()) return;
        
            if (configureOptions is not null)
            {
                app.MapScalarApiReference((scalarOptions) =>
                {
                    scalarOptions.CopyFrom(options);
                });

                app.MapSwaggerApiReference();
            }
            else
            {
                app.MapScalarApiReference();
            }
        
            app.UseDeveloperExceptionPage();
        };

        InvokeOrListen(action.Listener);

        return Application;
    }

    public WebApplication Build()
    {
        if (Application is not null) return Application;
        Application = Builder.Build();
        Application.UseApiServices(Environment);
        return Application;
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
        Application!.Run();
    }
    
    public async Task RunAsync([StringSyntax("Uri")] string? url = null)
    {
        BuildAndEmitListen();
        await Application!.RunAsync(url);
    }
    
    public void Start()
    {
        BuildAndEmitListen();
        Application!.Start();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        BuildAndEmitListen();
        await Application!.StartAsync(cancellationToken);
    }
}