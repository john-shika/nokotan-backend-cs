using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Models;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils.Net;
using TagNames = NokoWebApiSdk.OpenApi.NokoWebOpenApiSecuritySchemeTagNames;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    private readonly UserRepository _userRepository;

    public AppController(IConfiguration configuration, ILogger<AuthController> logger, UserRepository userRepository)
    {
        _configuration = configuration;
        _logger = logger;
        _userRepository = userRepository;
    }
    
    [HttpGet("users")]
    [Tags(TagNames.Anonymous, "App")]
    [EndpointSummary("GET_ALL_USERS")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        var messageBody = new MessageBody<List<User>>
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCodes.Ok,
            Status = HttpStatusText.FromCode(HttpStatusCodes.Ok),
            Timestamp = NokoWebCommon.GetDateTimeUtcNowInMilliseconds(),
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
            StatusCode = (int)HttpStatusCodes.Ok,
            Status = HttpStatusText.FromCode(HttpStatusCodes.Ok),
            Timestamp = NokoWebCommon.GetDateTimeUtcNowInMilliseconds(),
            Message = "Hello World.",
            Data = null,
        };
        
        return TypedResults.Ok(messageBody);
    }
}