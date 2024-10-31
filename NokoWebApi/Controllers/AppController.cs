using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Controllers;
using NokoWebApiSdk.Cores.Networking;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;
using TagNames = NokoWebApiSdk.OpenApi.NokoWebOpenApiSecuritySchemeTagNames;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class AppController : BaseApiController
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AppController> _logger;
    private readonly UserRepository _user;

    public AppController(IConfiguration configuration, ILogger<AppController> logger, UserRepository user)
    {
        _configuration = configuration;
        _logger = logger;
        _user = user;
    }
    
    [HttpGet("message")]
    [Tags(TagNames.Anonymous, "App")]
    [EndpointSummary("GET_MESSAGE")]
    [Produces<EmptyMessageBody>]
    public Task<IResult> GetMessage()
    {
        var messageBody = new EmptyMessageBody
        {
            StatusOk = true,
            StatusCodes = NokoHttpStatusCodes.Ok,
            Status = NokoHttpStatusCodes.Ok.ToString(),
            Timestamp = NokoCommonMod.GetDateTimeUtcNow(),
            Message = "Hello World.",
            Data = null,
        };

        var options = new JsonSerializerOptions();
        JsonSerializerService.Apply(options);
        return Task.FromResult(Results.Json(data: messageBody, options: options, statusCode: (int)NokoHttpStatusCodes.Ok));
    }
}