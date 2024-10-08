using Microsoft.AspNetCore.Mvc;
using NokoWebApi.Schemas;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils;
using NokoWebApiSdk.Utils.Net;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController
{
    [HttpGet]
    public async Task<IResult> GetMessage()
    {
        var messageBody = new MessageBody<object>
        {
            StatusOk = true,
            StatusCode = (int)HttpStatusCodes.Ok,
            Status = HttpStatusText.From(HttpStatusCodes.Ok),
            Timestamp = Common.GetDateTimeUtcNowInMilliseconds(),
            Message = "Hello World",
            Data = null,
        };
        
        return TypedResults.Ok(messageBody);
    }
}