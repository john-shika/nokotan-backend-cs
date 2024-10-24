using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Globals;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Services;

[AppService]
public class AuthService : AppServiceInitialized
{
    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        NokoWebApplicationGlobals.JwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var secretKey = NokoWebCommonMod.EncodeSha512(NokoWebApplicationGlobals.GetJwtSecretKey());
        var issuer = NokoWebApplicationGlobals.GetJwtIssuer();
        var audience = NokoWebApplicationGlobals.GetJwtAudience();
        
        services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                };
            });
    }
}