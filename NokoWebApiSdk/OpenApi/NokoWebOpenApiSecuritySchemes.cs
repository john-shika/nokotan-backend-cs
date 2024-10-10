using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace NokoWebApiSdk.OpenApi;

public static class NokoWebOpenApiSecuritySchemes
{
    public static readonly OpenApiSecurityScheme SecuritySchemeJwt = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please enter token in following format: Bearer <JWT>",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = NokoWebOpenApiSecuritySchemeTagNames.BearerJwt,
        }
    };
}