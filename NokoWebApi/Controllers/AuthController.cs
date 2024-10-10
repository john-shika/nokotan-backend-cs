using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using NokoWebApi.Repositories;
using NokoWebApi.Schemas;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.OpenApi;
using NokoWebApiSdk.Utils.Net;
using JwtClaimTagNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using TagNames = NokoWebApiSdk.OpenApi.NokoWebOpenApiSecuritySchemeTagNames;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    private readonly SessionRepository _sessionRepository;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger, SessionRepository sessionRepository)
    {
        _configuration = configuration;
        _logger = logger;
        _sessionRepository = sessionRepository;
    }
    
    // <summary>
    // This is Login API Reference.
    // </summary>
    // <param name="loginFormBody">This is Login Form Body Expected.</param>
    // <returns>Returns a message body with access token generated.</returns>
    [HttpPost("login")]
    [Tags(TagNames.Anonymous, "Auth")]
    [EndpointSummary("LOGIN_USER")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IResult> Authenticate([FromBody] LoginFormBody loginFormBody)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        
        _logger.LogInformation($"IpAddress: {ipAddress}");
        _logger.LogInformation($"UserAgent: {userAgent}");
        
        var token = GenerateJwtToken(
            NokoWebCommon.GenerateUuidV7(), 
            loginFormBody.Username, 
            DateTime.UtcNow.AddDays(7));
        
        var messageBody = new AccessJwtTokenMessageBody
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCodes.Created,
            Status = HttpStatusText.FromCode(HttpStatusCodes.Created),
            Timestamp = NokoWebCommon.GetDateTimeUtcNowInMilliseconds(),
            Message = "Successfully create JWT Token.",
            Data = new AccessJwtTokenData {
                AccessToken = token,
            },
        };
        
        return TypedResults.Ok(messageBody);
    }
    
    [HttpGet("validate")]
    [Tags(TagNames.BearerJwt, TagNames.RoleAdmin, "Auth")]
    [EndpointSummary("VALIDATE_USER")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IResult> ValidateToken([FromHeader(Name = "Authorization")] string authorization)
    {
        // var token = HttpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault()?
        var token = authorization.Split(" ").Last()!;
        
        try
        {
            var (sessionId, username, expires) = ParseJwtToken(token);
            return TypedResults.Ok(new
            {
                SessionId = sessionId,
                Username = username,
                Expires = expires
            });
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new { Message = ex.Message });
        }
    }

    private string GenerateJwtToken(Guid sessionId, string username, DateTime? expires = null)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);
        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        
        var tokenHandler = new JwtSecurityTokenHandler();

        const string algorithm = SecurityAlgorithms.HmacSha256Signature;

        var jti = NokoWebCommon.GenerateUuidV7();
        var symmetricSecurityKey = new SymmetricSecurityKey(secretKey);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtClaimTagNames.Jti, jti.ToString()),
                new Claim(JwtClaimTagNames.Sid, sessionId.ToString()),
                new Claim("username", username),
                // new Claim("role", "user"),
                new Claim("role", "Admin"),
            ]),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials,
        };
        
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private (Guid SessionId, string Username, DateTime? Expires) ParseJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
        {
            throw new ArgumentException("Invalid JWT token");
        }

        var sessionId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtClaimTagNames.Sid).Value);
        var username = jwtToken.Claims.First(claim => claim.Type == "username").Value;
        var expires = jwtToken.ValidTo;

        return (sessionId, username, expires);
    }

}
