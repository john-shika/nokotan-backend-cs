using System.Text;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Globals;

public class NokoWebApplicationGlobals
{
    public static JwtSettings? JwtSettings { get; set; }

    public NokoWebApplicationGlobals(IConfiguration configuration)
    {
        JwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
    }

    public static string GetJwtSecretKey() 
    {
        var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        return jwtSecretKey ?? JwtSettings?.SecretKey ?? NokoWebCommonMod.GenerateRandomString(128);
    }
    
    public static string GetJwtIssuer() 
    {
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        return jwtIssuer ?? JwtSettings?.Issuer ?? "localhost";
    }
    
    public static string GetJwtAudience() 
    {
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        return jwtAudience ?? JwtSettings?.Audience ?? "localhost";
    }
}