﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.OpenApi.Extensions;

namespace NokoWebApiSdk.OpenApi.Transformers;

public sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    private static INokoLogger Logger => new NokoLogger();
    
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        
        var defineAuthSchemeSecurities = NokoWebOpenApiDefaults.AuthSchemeBearerTagOpenApiSecuritySchemes;
        var defineAuthSchemeFounds = NokoCommonMod.MergeAnyMapValues(defineAuthSchemeSecurities, false);

        document.Preload();

        foreach (var authScheme in authSchemes)
        {
            var defineAuthSchemeWhereFounds = defineAuthSchemeFounds
                .Where(defineAuthSchemeFound => authScheme.Name == defineAuthSchemeFound.Key);
            
            foreach (var defineAuthSchemeFound in defineAuthSchemeWhereFounds)
            {
                defineAuthSchemeFounds[defineAuthSchemeFound.Key] = true;
            }
        }

        foreach (var (authScheme, (tagName, securityScheme)) in defineAuthSchemeSecurities)
        {
            var defineAuthSchemeAnyFounds = defineAuthSchemeFounds
                .Any((defineAuthSchemeFound) => authScheme == defineAuthSchemeFound.Key && defineAuthSchemeFound.Value);
            
            if (!defineAuthSchemeAnyFounds) continue;
            
            var securityRequirement = new OpenApiSecurityRequirement
            {
                [securityScheme] = []
            };
            
            document.Components.SecuritySchemes[tagName] = securityScheme;
            NokoCommonMod.InsertAnyItemList(document.SecurityRequirements, securityRequirement);
            
            foreach (var (path, item) in document.Paths)
            {
                foreach (var (operationType, operation) in item.Operations)
                {
                    var tag = operation.Tags.FirstOrDefault((t) => t.Name == tagName);
                    if (tag is null) continue;
                    
                    Logger.Warning($"Set Scheme Security On Method: {operationType}, Path: '{path}'");
                    NokoCommonMod.InsertAnyItemList(operation.Security, securityRequirement);
                }
            }
        }
    }
}