using System.Text;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions;
using NokoWebApiSdk.Extensions.ConfigurationBinder;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Globals;

public class NokoWebApplicationGlobals
{
    public static JwtSettings? JwtSettings { get; set; }

    public NokoWebApplicationGlobals(IConfiguration configuration)
    {
        JwtSettings = configuration.GetConfig<JwtSettings>();
    }

    public static string GetJwtSecretKey() 
    {
        var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(jwtSecretKey))
        {
            return JwtSettings?.SecretKey ?? NokoCommonMod.GenerateRandomString(128);
        }

        return jwtSecretKey!;
    }
    
    public static string GetJwtIssuer() 
    {
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(jwtIssuer))
        {
            return JwtSettings?.Issuer ?? "localhost";
        }

        return jwtIssuer!;
    }
    
    public static string GetJwtAudience() 
    {
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(jwtAudience))
        {
            return JwtSettings?.Audience ?? "localhost";
        }

        return jwtAudience!;
    }
}