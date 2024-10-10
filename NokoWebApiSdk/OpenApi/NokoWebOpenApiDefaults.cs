using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace NokoWebApiSdk.OpenApi;

public class AuthSchemeBearerTagOpenApiSecurityScheme : Dictionary<string, (string, OpenApiSecurityScheme)>;

public static class NokoWebOpenApiDefaults
{
    public static AuthSchemeBearerTagOpenApiSecurityScheme AuthSchemeBearerTagOpenApiSecuritySchemes = new() 
    {
        [JwtBearerDefaults.AuthenticationScheme] = (NokoWebOpenApiSecuritySchemeTagNames.BearerJwt, NokoWebOpenApiSecuritySchemes.SecuritySchemeJwt),
    };
}