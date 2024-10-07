using Microsoft.EntityFrameworkCore;
using NokoWebApi.Controllers;
using NokoWebApiExtra.Extensions.ApiService;
using NokoWebApi.Services;
using NokoWebApiExtra.Extensions.ApiRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiRepositories((options) =>
{
    // options.UseInMemoryDatabase("main");
    options.UseSqlite("Data Source=Migrations/dev.db");
});

builder.Services.AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
