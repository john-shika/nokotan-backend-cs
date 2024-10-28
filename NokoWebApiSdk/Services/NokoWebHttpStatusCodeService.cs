﻿using System.Text.Json;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Net;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Extensions.AppService;
using NokoWebApiSdk.Json.Services;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Services;

[AppService]
public class NokoWebHttpStatusCodeService : AppServiceInitialized
{
    public override void OnConfigure(WebApplication application, IWebHostEnvironment environment) 
    {
        application.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;
            switch (response.StatusCode)
            {
                case (int)NokoWebHttpStatusCode.NotFound:
                    response.ContentType = "application/json";
                    
                    var messageBody = new EmptyMessageBody
                    {
                        StatusOk = false,
                        StatusCode = response.StatusCode,
                        Status = NokoWebHttpStatusCode.NotFound.ToString(),
                        Timestamp = NokoWebCommonMod.GetDateTimeUtcNow(),
                        Message = "Resource not found",
                    };

                    var options = new JsonSerializerOptions();
                    JsonSerializerService.Apply(options);
                    
                    var jsonResponse = JsonSerializer.Serialize(messageBody, options);
                    await response.WriteAsync(jsonResponse);
                    break;
                default:
                    break;
            }
        });
    }
}