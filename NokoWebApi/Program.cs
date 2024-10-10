using Microsoft.EntityFrameworkCore;
using NokoWebApi.Controllers;
using NokoWebApi.Repositories;
using NokoWebApiSdk;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Extensions.OpenApi.Scalar.Enums;

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
    options.Theme = OpenApiScalarTheme.Purple;
    options.Favicon = "/favicon.ico";
});

noko.Run();
