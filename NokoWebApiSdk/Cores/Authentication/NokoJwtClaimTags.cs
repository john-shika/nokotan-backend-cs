using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Authentication;

public enum NokoJwtClaimTags
{
    Jti,
    Sub,
    Sid,
    Aud,
    Iss,
    User,
    Email,
    Phone,
    Role,
    Nbf,
    Exp,
    Iat,
}

public static class NokoJwtClaimTagValues
{
    public const string Jti = "jti";
    public const string Sid = "sid";
    public const string Sub = "sub";
    public const string Aud = "aud";
    public const string Iss = "iss";
    public const string User = "user";
    public const string Email = "email";
    public const string Phone = "phone";
    public const string Role = "role";
    public const string Nbf = "nbf"; // optional, prefer using expires
    public const string Exp = "exp";
    public const string Iat = "iat";
    
    public static NokoJwtClaimTags ParseCode(string code)
    {
        return NokoTransformText.ToCamelCase(code) switch
        {
            Jti => NokoJwtClaimTags.Jti,
            Sid => NokoJwtClaimTags.Sid,
            Sub => NokoJwtClaimTags.Sub,
            Aud => NokoJwtClaimTags.Aud,
            Iss => NokoJwtClaimTags.Iss,
            User => NokoJwtClaimTags.User,
            Email => NokoJwtClaimTags.Email,
            Phone => NokoJwtClaimTags.Phone,
            Role => NokoJwtClaimTags.Role,
            Nbf => NokoJwtClaimTags.Nbf,
            Exp => NokoJwtClaimTags.Exp,
            Iat => NokoJwtClaimTags.Iat,
            _ => throw new FormatException($"Invalid jwt claim name {code}"),
        };
    }

    public static string FromCode(NokoJwtClaimTags code)
    {
        return code switch
        {
            NokoJwtClaimTags.Jti => Jti,
            NokoJwtClaimTags.Sid => Sid,
            NokoJwtClaimTags.Sub => Sub,
            NokoJwtClaimTags.Aud => Aud,
            NokoJwtClaimTags.Iss => Iss,
            NokoJwtClaimTags.User => User,
            NokoJwtClaimTags.Email => Email,
            NokoJwtClaimTags.Phone => Phone,
            NokoJwtClaimTags.Role => Role,
            NokoJwtClaimTags.Nbf => Nbf,
            NokoJwtClaimTags.Exp => Exp,
            NokoJwtClaimTags.Iat => Iat,
            _ => throw new Exception($"Unsupported jwt claim name {code}"),
        };
    }
}

public static class JwtClaimTagNameExtensions
{
    public static string GetKey(this NokoJwtClaimTags code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this NokoJwtClaimTags code)
    {
        return NokoJwtClaimTagValues.FromCode(code);
    }
}
