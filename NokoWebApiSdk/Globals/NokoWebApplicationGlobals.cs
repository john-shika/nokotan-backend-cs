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
        var cfgJwtSecretKey = JwtSettings?.SecretKey;
        var envJwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        var ranJwtSecretKey = NokoWebCommonMod.GenerateRandomString(128); // 1kb
        return envJwtSecretKey ?? cfgJwtSecretKey ?? ranJwtSecretKey;
    }
    
    public static string GetJwtIssuer() 
    {
        var cfgJwtIssuer = JwtSettings?.Issuer;
        var envJwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        const string ranJwtIssuer = "localhost";
        return envJwtIssuer ?? cfgJwtIssuer ?? ranJwtIssuer;
    }
    
    public static string GetJwtAudience() 
    {
        var cfgJwtAudience = JwtSettings?.Audience;
        var envJwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        const string ranJwtAudience = "localhost";
        return envJwtAudience ?? cfgJwtAudience ?? ranJwtAudience;
    }
}