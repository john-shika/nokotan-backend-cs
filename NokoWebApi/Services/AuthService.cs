using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Extensions.ConfigurationBinder;
using NokoWebApiSdk.Globals;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Services;

[AppService]
public class AuthService : AppServiceInitialized
{
    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        NokoWebApplicationGlobals.JwtSettings = configuration.GetConfig<JwtSettings>();
        
        var secretKey = NokoWebApplicationGlobals.GetJwtSecretKey();
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
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    ValidateAudience = false, // disabling audience validation as multiple audience values are managed independently
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(NokoCommonMod.EncodeSha512(secretKey)),
                };
            });
    }
}