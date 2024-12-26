using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Globals;

namespace NokoWebApiSdk.Cores.Authentication;

public static class NokoAuthenticationMod
{
    public static string GenerateJwtToken(NokoWebToken nokoWebToken)
    {
        var secretKey = NokoWebApplicationGlobals.GetJwtSecretKey();
        var issuer = NokoWebApplicationGlobals.GetJwtIssuer();
        var audience = NokoWebApplicationGlobals.GetJwtAudience();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        const string algorithm = SecurityAlgorithms.HmacSha256Signature;

        var claims = new[] {
            new Claim(NokoJwtClaimTags.Jti.GetValue(), nokoWebToken.Id.ToString()),
            new Claim(NokoJwtClaimTags.Sid.GetValue(), nokoWebToken.SessionId.ToString()),
            new Claim(NokoJwtClaimTags.User.GetValue(), nokoWebToken.User ?? "Anonymous"),
            new Claim(NokoJwtClaimTags.Email.GetValue(), nokoWebToken.Email ?? ""),
            new Claim(NokoJwtClaimTags.Phone.GetValue(), nokoWebToken.Phone ?? ""),
            new Claim(NokoJwtClaimTags.Role.GetValue(), nokoWebToken.Role ?? "Guest"),
        };
        
        var symmetricSecurityKey = new SymmetricSecurityKey(NokoCommonMod.EncodeSha512(secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            // Audience = audience,
            Issuer = issuer,
            Expires = nokoWebToken.Expires,
            SigningCredentials = signingCredentials,
        };

        if (nokoWebToken.NotBefore is not null && nokoWebToken.NotBefore != DateTime.MinValue)
        {
            tokenDescriptor.NotBefore = nokoWebToken.NotBefore;
        }

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var audiences = new List<string>{audience};
        
        NokoCommonMod.MergeAnyItemsList(audiences, nokoWebToken.Audiences);
        
        token.Payload.Add(NokoJwtClaimTags.Aud.GetValue(), string.Join(",", audiences));
        
        return tokenHandler.WriteToken(token);
    }

    public static NokoWebToken ParseJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.ReadToken(token) is not JwtSecurityToken jwtToken)
        {
            throw new ArgumentException("Invalid JWT token");
        }

        var claims = jwtToken.Claims;

        if (claims is null)
        {
            throw new NullReferenceException("No claims found");
        }

        var data = claims as Claim[] ?? claims.ToArray();
        var jti = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Jti.GetValue())!.Value;
        var sub = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Sub.GetValue())?.Value;
        var sid = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Sid.GetValue())!.Value;
        var aud = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Aud.GetValue())!.Value.Split(",");
        var iss = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Iss.GetValue())!.Value;
        var user = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.User.GetValue())?.Value;
        var email = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Email.GetValue())?.Value;
        var phone = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Phone.GetValue())?.Value;
        var role = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Role.GetValue())?.Value;
        var nbf = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Nbf.GetValue())?.Value;
        var exp = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Exp.GetValue())!.Value;
        var iat = data.FirstOrDefault((claim) => claim.Type == NokoJwtClaimTags.Iat.GetValue())!.Value;

        Console.WriteLine($"jti: {jti}");
        Console.WriteLine($"sub: {sub}");
        Console.WriteLine($"sid: {sid}");
        Console.WriteLine($"aud: {aud}");
        Console.WriteLine($"iss: {iss}");
        Console.WriteLine($"user: {user}");
        Console.WriteLine($"email: {email}");
        Console.WriteLine($"phone: {phone}");
        Console.WriteLine($"role: {role}");
        Console.WriteLine($"nbf: {nbf}");
        Console.WriteLine($"exp: {exp}");
        Console.WriteLine($"iat: {iat}");
        
        var nokoWebToken = new NokoWebToken();

        return nokoWebToken;
    }
}