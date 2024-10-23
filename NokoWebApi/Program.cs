using Microsoft.EntityFrameworkCore;
using NokoWebApi.Controllers;
using NokoWebApi.Repositories;
using NokoWebApiSdk;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Extensions.NokoWebApi;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

var noko = NokoWebApplication.Create(args);

noko.Repository((options) =>
{
    // options.UseInMemoryDatabase("main");
    options.UseSqlite("Data Source=Migrations/dev.db");
});

noko.MapOpenApi((options) =>
{
    options.Title = "Scalar API Reference -- {documentName}";
    options.EndpointPathPrefix = "/scalar/{documentName}";
    options.OpenApiRoutePattern = "/openapi/{documentName}.json";
    // options.CdnUrl = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";
    options.CdnUrl = "/js/scalar.api-reference.js";
    options.Theme = ScalarOpenApiTheme.BluePlanet;
    options.Favicon = "/favicon.ico";
});

// Roslyn Source Generator with Reflection 
// must be using external command to generate
// using like dotnet run noko optimize
// noko.Optimize((options) =>
// {
//     options.DirPath = "Optimizes";
//     options.Namespace = "NokoWebApi.Optimizes";
//     options.Cached = true;
// });
// then direct server using type generic
// noko.Run<NokoWebApi.Optimizes.Program>()

// load any environment variables and config files
noko.UseGlobals();

noko.Run();
