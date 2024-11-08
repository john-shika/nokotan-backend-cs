﻿using NokoWebApi.Controllers;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Generator.Helper;
using NokoWebApiSdk.OpenApi;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Optimizes;

public partial class EntryPointAutoGenerated : NokoWebSourceGeneratorEntryPoint
{
    public override void OnInitialized(NokoWebApplication nokoWebApplication)
    {
        // Class: [AppController]
        nokoWebApplication.Listen((nokoWebApplication) =>
        {
            var app = nokoWebApplication.Application;
            var serviceProviders = app.Services;

            using var serviceScope = serviceProviders.CreateScope();
            
            var serviceProvider = serviceScope.ServiceProvider;

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var logger = serviceProvider.GetRequiredService<ILogger<AppController>>();
            var user = serviceProvider.GetRequiredService<UserRepository>();
            
            var parameters = new object[]
            {
                serviceProvider.GetRequiredService<IConfiguration>(),
                serviceProvider.GetRequiredService<ILogger<AppController>>(),
                serviceProvider.GetRequiredService<UserRepository>(),
            };
        
            var controller = new AppController((IConfiguration)parameters[0], (ILogger<AppController>)parameters[1], (UserRepository)parameters[2]);
        
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