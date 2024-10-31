using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using NokoWebApi.Repositories;
using NokoWebApi.Schemas;
using NokoWebApiSdk.Controllers;
using NokoWebApiSdk.Cores.Authentication;
using NokoWebApiSdk.Cores.Networking;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions.ConfigurationBinder;
using NokoWebApiSdk.Globals;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;
using JwtClaimTagNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using TagNames = NokoWebApiSdk.OpenApi.NokoWebOpenApiSecuritySchemeTagNames;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : BaseApiController
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    private readonly SessionRepository _session;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger, SessionRepository session)
    {
        _configuration = configuration;
        _logger = logger;
        _session = session;
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
    [Produces<AccessJwtTokenMessageBody>]
    public Task<IResult> Authenticate([FromBody] LoginFormBody loginFormBody)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        
        _logger.LogInformation($"IpAddress: {ipAddress}");
        _logger.LogInformation($"UserAgent: {userAgent}");

        var tokenId = NokoCommonMod.GenerateUuidV7(); // as JTI
        var sessionId = NokoCommonMod.GenerateUuidV7(); // as SID
        var user = loginFormBody.Username; // as USERNAME
        var expires = DateTime.UtcNow.AddDays(7); // as EXP

        var nokoWebToken = new NokoWebToken
        {
            Id = tokenId,
            SessionId = sessionId,
            User = user,
            Role = "Admin",
            Expires = expires,
            Audiences = ["localhost:80", "localhost:3000", "localhost:5000"],
        };
        
        var token = NokoAuthenticationMod.GenerateJwtToken(nokoWebToken);
        
        var messageBody = new AccessJwtTokenMessageBody
        {
            StatusOk = true,
            StatusCodes = NokoHttpStatusCodes.Created,
            Status = NokoHttpStatusCodes.Created.ToString(),
            Timestamp = NokoCommonMod.GetDateTimeUtcNow(),
            Message = "Successfully create JWT Token.",
            Data = new AccessJwtTokenData {
                AccessToken = token,
            },
        };
        
        return Task.FromResult<IResult>(TypedResults.Ok(messageBody));
    }
    
    [HttpGet("validate")]
    [Tags(TagNames.BearerJwt, TagNames.RoleAdmin, "Auth")]
    [EndpointSummary("VALIDATE_USER")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    [Produces<ValidateJwtTokenMessageBody>]
    public Task<IResult> ValidateToken([FromHeader(Name = "Authorization")] string authorization)
    {
        // var token = HttpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault()?
        var token = authorization.Split(" ").Last()!;

        var messageBody = new ValidateJwtTokenMessageBody();
        messageBody.StatusOk = true;
        messageBody.StatusCodes = NokoHttpStatusCodes.Ok;
        messageBody.Status = NokoHttpStatusCodes.Ok.GetValue();
        messageBody.Timestamp = NokoCommonMod.GetDateTimeUtcNow();
        messageBody.Message = "Successfully validate JWT Token.";
        
        try
        {
            NokoAuthenticationMod.ParseJwtToken(token);
            var (tokenId, sessionId, username, expires) = ParseJwtToken(token);
            var data = new ValidateJwtTokenData
            {
                TokenId = tokenId,
                SessionId = sessionId,
                Username = username,
                Expires = expires,
                // IssuedAt = 
            };

            messageBody.Data = data;
            
            var options = new JsonSerializerOptions();
            JsonSerializerService.Apply(options);
            var result = TypedResults.Json(data: messageBody, options: options, statusCode: (int)messageBody.StatusCodes);
            return Task.FromResult<IResult>(result);
        }
        catch (Exception ex)
        {
            messageBody.StatusOk = false;
            messageBody.StatusCodes = NokoHttpStatusCodes.InternalServerError;
            messageBody.Status = NokoHttpStatusCodes.InternalServerError.ToString();
            messageBody.Message = ex.Message;
            return Task.FromResult<IResult>(TypedResults.BadRequest(messageBody));
        }
    }

    private static string GenerateJwtToken(Guid tokenId, Guid sessionId, string username, DateTime? expires = null)
    {
        var secretKey = NokoWebApplicationGlobals.GetJwtSecretKey();
        var issuer = NokoWebApplicationGlobals.GetJwtIssuer();
        var audience = NokoWebApplicationGlobals.GetJwtAudience();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        const string algorithm = SecurityAlgorithms.HmacSha256Signature;

        var claims = new[] {
            new Claim(JwtClaimTagNames.Jti, tokenId.ToString()),
            new Claim(JwtClaimTagNames.Sid, sessionId.ToString()),
            new Claim("user", username),
            new Claim("role", "Admin"),
        };
        
        var symmetricSecurityKey = new SymmetricSecurityKey(NokoCommonMod.EncodeSha512(secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials,
        };
        
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private (Guid TokenId, Guid SessionId, string Username, DateTime Expires) ParseJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
        {
            throw new ArgumentException("Invalid JWT token");
        }

        var tokenId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtClaimTagNames.Jti).Value);
        var sessionId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtClaimTagNames.Sid).Value);
        var username = jwtToken.Claims.First(claim => claim.Type == "user").Value;
        var expires = jwtToken.ValidTo;

        return (tokenId, sessionId, username, expires);
    }
}
