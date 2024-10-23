using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Models;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
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
    
    [HttpGet("users")]
    [Tags(TagNames.Anonymous, "App")]
    [EndpointSummary("GET_ALL_USERS")]
    [Produces(typeof(MessageBody<List<User>>))]
    public async Task<IResult> GetAllUsers()
    {
        var users = await _user.GetAllUsers();
        var messageBody = new MessageBody<List<User>>
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCode.Ok,
            Status = HttpStatusCode.Ok.ToString(),
            Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
            Message = "Get All Users.",
            Data = users,
        };
        
        return TypedResults.Ok(messageBody);
    }
    
    [HttpGet("message")]
    [Tags(TagNames.Anonymous, "App")]
    [EndpointSummary("GET_MESSAGE")]
    [Produces(typeof(EmptyMessageBody))]
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
        
        return TypedResults.Ok(messageBody);
    }
}