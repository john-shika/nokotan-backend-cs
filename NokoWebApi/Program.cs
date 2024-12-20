using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Controllers;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions.NokoWebApi;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Filters;
using NokoWebApiSdk.Generator.Extensions;
using NokoWebApiSdk.Globals;
using NokoWebApiSdk.Middlewares;

// var currDomainBaseDir = AppDomain.CurrentDomain.BaseDirectory;
// Console.WriteLine("Program Directory: " + currDomainBaseDir);
//
// var currWorkDir = Directory.GetCurrentDirectory();
// Console.WriteLine("Current Working Directory: " + currWorkDir);
//
// var entryAssembly = Assembly.GetEntryAssembly()!;
// var @namespace = entryAssembly.GetName().Name;
// Console.WriteLine($"Namespace (AssemblyName): {@namespace}");
//
// // Get Assemblies In Current App Domain
// var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
//         
// // Get Executing Assembly In Internal Assembly
// var executingAssembly = Assembly.GetExecutingAssembly();
// if (!assemblies.Contains(executingAssembly))
// {
//     assemblies.Add(executingAssembly);
// }
//
// var baseApiControllerType = typeof(BaseApiController);
//
// var types = assemblies
//     .SelectMany((assembly) => assembly.GetTypes())
//     .Where((type) =>
//     {
//         var isAssignable = baseApiControllerType.IsAssignableFrom(type); 
//         var hasAttribute = NokoCommonMod.HasAttribute<ApiControllerAttribute>(type);
//         return type is { IsClass: true, IsPublic: true } && hasAttribute && isAssignable;
//     });
//
// foreach (var type in types)
// {
//     var temp = new StringBuilder();
//     temp.AppendLine($"Namespace: {NokoCommonMod.TrimLastNamespaceSegment(type.Namespace)}");
//     temp.AppendLine($"Controller: {type.Namespace} {type.Name}");
//     
//     var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
//         .Where((method) =>
//         {
//             var attributes = method.GetCustomAttributes();
//             return attributes.Any((attribute) => attribute is HttpMethodAttribute);
//         });
//
//     var constructor = type.GetConstructors().FirstOrDefault();
//
//     if (constructor is not null)
//     {
//         var parameters = constructor.GetParameters();
//         foreach (var parameter in parameters)
//         {
//             temp.AppendLine($" + {parameter.Name}: {parameter.ParameterType.Namespace} {NokoCommonMod.StripGenericMarker(parameter.ParameterType)}");
//             if (parameter.ParameterType.IsGenericType)
//             {
//                 var arguments = parameter.ParameterType.GetGenericArguments();
//                 foreach (var argument in arguments)
//                 {
//                     temp.AppendLine($"  - {argument.Namespace} {argument.Name}");
//                 }
//             }
//         }
//     }
//
//     foreach (var method in methods)
//     {
//         var httpMethodAttribute = NokoCommonMod.GetAttribute<HttpMethodAttribute>(method);
//         var httpMethods = httpMethodAttribute!.HttpMethods;
//
//         foreach (var httpMethod in httpMethods)
//         {
//             temp.AppendLine($"- Method: {method.Name} HttpMethod: {httpMethod}");
//         }
//
//         var authorizeAttribute = NokoCommonMod.GetAttribute<AuthorizeAttribute>(method);
//
//         if (authorizeAttribute is not null)
//         {
//             temp.AppendLine($"- Method: {method.Name} Authorize");
//         }
//     }
//     Console.WriteLine(temp.ToString());
// }
//
// return;

var noko = NokoWebApplication.Create(args);

noko.Repository((options) =>
{
    // options.UseInMemoryDatabase("main");
    options.UseSqlite("Data Source=Migrations/dev.db");
});

noko.Listen((nk) =>
{
    var app = nk.Application!;
    if (!app.Environment.IsDevelopment()) return;
    
    var serviceScope = app.Services.CreateScope();
    var userRepository = serviceScope.ServiceProvider.GetRequiredService<UserRepository>();
    var sessionRepository = serviceScope.ServiceProvider.GetRequiredService<SessionRepository>();
    
    using var _ = new AutoDisposableObjects(serviceScope, userRepository, sessionRepository);

    userRepository.Database.EnsureCreated();
    sessionRepository.Database.EnsureCreated();
});

noko.MapOpenApi((options) =>
{
    options.Title = "Scalar API Reference -- {documentName}";
    options.EndpointPathPrefix = "/scalar/{documentName}";
    options.OpenApiRoutePattern = "/openapi/{documentName}.json";
    // options.CdnUrl = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";
    options.CdnUrl = "/js/scalar.api-reference.js";
    options.Themes = ScalarOpenApiThemes.BluePlanet;
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

// noko.Listen((nk) =>
// {
//     var entryPointAutoGenerated = new NokoWebApi.Optimizes.EntryPointAutoGenerated();
//     entryPointAutoGenerated.OnInitialized(nk);
// });

// noko.EntryPoint<NokoWebApi.Optimizes.EntryPointAutoGenerated>();

noko.Build();

noko.Application!.Use(HttpExceptionMiddleware.Handler);
noko.Application!.UseMiddleware<CustomExceptionMiddleware>();

noko.Run();
