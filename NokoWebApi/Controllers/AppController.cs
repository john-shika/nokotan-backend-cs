using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Models;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils;
using NokoWebApiSdk.Utils.Net;

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
    [Tags("App")]
    [EndpointSummary("Get All Users")]
    [EndpointDescription("Get All Users Endpoint")]
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
    [Tags("App")]
    [EndpointSummary("Get Message")]
    [EndpointDescription("Get Message Endpoint")]
    [Produces(typeof(MessageBody<object>))]
    public async Task<IResult> GetMessage()
    {
        var messageBody = new MessageBody<object>
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