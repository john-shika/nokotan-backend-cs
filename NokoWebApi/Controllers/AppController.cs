using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Models;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Converters;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;
using TagNames = NokoWebApiSdk.OpenApi.NokoWebOpenApiSecuritySchemeTagNames;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    private readonly UserRepository _user;

    public AppController(IConfiguration configuration, ILogger<AuthController> logger, UserRepository user)
    {
        _configuration = configuration;
        _logger = logger;
        _user = user;
    }
    
    [HttpGet("message")]
    [Tags(TagNames.Anonymous, "App")]
    [EndpointSummary("GET_MESSAGE")]
    [Produces<EmptyMessageBody>]
    public async Task<IResult> GetMessage()
    {
        var messageBody = new EmptyMessageBody
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCode.Ok,
            Status = HttpStatusCode.Ok.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = "Hello World.",
            Data = null,
        };

        var options = new JsonSerializerOptions();
        JsonService.JsonSerializerConfigure(options);
        return Results.Json(data: messageBody, options: options, statusCode: (int)HttpStatusCode.Ok);
    }
}