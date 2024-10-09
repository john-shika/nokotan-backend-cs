using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Models;
using NokoWebApi.Repositories;
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
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        var messageBody = new MessageBody<List<User>>
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCodes.Ok,
            Status = HttpStatusText.FromCode(HttpStatusCodes.Ok),
            Timestamp = Common.GetDateTimeUtcNowInMilliseconds(),
            Message = "Get All Users.",
            Data = users,
        };
        
        return TypedResults.Ok(messageBody);
    }
    
    [HttpGet("message")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IResult> GetMessage()
    {
        var messageBody = new MessageBody<object>
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCodes.Ok,
            Status = HttpStatusText.FromCode(HttpStatusCodes.Ok),
            Timestamp = Common.GetDateTimeUtcNowInMilliseconds(),
            Message = "Hello World.",
            Data = null,
        };
        
        return TypedResults.Ok(messageBody);
    }
}