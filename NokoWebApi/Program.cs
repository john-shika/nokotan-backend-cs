using Microsoft.EntityFrameworkCore;
using NokoWebApi.Controllers;
using NokoWebApi.Modules;
using NokoWebApi.Repositories;
using NokoWebApiSdk;
using NokoWebApiSdk.Extensions.Scalar.Enums;

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
    options.Theme = ScalarTheme.Purple;
    options.Favicon = "/favicon.ico";
});

noko.Run();
