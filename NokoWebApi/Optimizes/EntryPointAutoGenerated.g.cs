﻿using NokoWebApi.Controllers;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Generator.Helper;
using NokoWebApiSdk.OpenApi;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Optimizes;

public partial class EntryPointAutoGenerated : NokoWebSourceGeneratorEntryPoint
{
    public override void OnInitialized(NokoWebApplication noko)
    {
        // Class: [AppController]
        noko.Listen((nk) =>
        {
            var app = nk.Application;
            var services = app.Services;

            var serviceScope = services.CreateScope();
            
            var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<AppController>>();
            var userRepository = serviceScope.ServiceProvider.GetRequiredService<UserRepository>();
            
            using var _ = new AutoDisposableObjects(serviceScope, configuration, logger, userRepository);
        
            var controller = new AppController(configuration, logger, userRepository);
        
            var group = app.MapGroup("api/v1");
        
            group.MapGet("message", controller.GetMessage)
                .AllowAnonymous()
                .WithTags([
                    "Anonymous", 
                    "App",
                ])
                .WithSummary("GET_MESSAGE")
                .Produces<EmptyMessageBody>();
        });
    }
}