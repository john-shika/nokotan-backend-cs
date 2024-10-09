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
    public WebApplicationBuilder ApplicationBuilder { get; init; }
    public IServiceCollection Services => ApplicationBuilder.Services;
    public WebApplication? Application { get; set; }
    public IWebHostEnvironment? Environment => Application?.Environment;
    public IHostApplicationLifetime? ApplicationLifetime => Application?.Lifetime;
    public ILogger? Logger => Application?.Logger;
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
    public WebApplicationBuilder ApplicationBuilder { get; init; }
    public IServiceCollection Services => ApplicationBuilder.Services;
    public WebApplication? Application { get; set; }
    
    public IWebHostEnvironment? Environment => Application?.Environment;
    public IHostApplicationLifetime? ApplicationLifetime => Application?.Lifetime;
    public ILogger? Logger => Application?.Logger;

    public List<Action<NokoWebApplication>?> Listeners { get; }
    
    public NokoWebApplication(string[] args)
    {
        ApplicationBuilder = WebApplication.CreateBuilder(args);
        ApplicationBuilder.Services.AddApiModules(ApplicationBuilder.Configuration);
        ApplicationBuilder.Services.AddOpenApi();
        Listeners = [];
    }

    protected void InvokeOrListen(Action<NokoWebApplication> listener)
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

    public WebApplication? MapOpenApi(Action<ScalarOptions>? configureOptions = null)
    {
        var listener = (NokoWebApplication noko) =>
        {
            var app = noko.Application!;
            var env = noko.Environment!;
            
            var options = new ScalarOptions();
            configureOptions?.Invoke(options);
        
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
            }
            else
            {
                app.MapScalarApiReference();
            }
        
            app.UseDeveloperExceptionPage();
        };

        InvokeOrListen(listener);

        return Application;
    }

    public WebApplication Build()
    {
        if (Application is not null) return Application;
        Application = ApplicationBuilder.Build();
        Application.UseApiModules(Environment);
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