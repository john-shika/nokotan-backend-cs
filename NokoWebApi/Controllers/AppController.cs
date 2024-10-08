using Microsoft.AspNetCore.Mvc;
using NokoWebApiSdk.Schemas;
using NokoWebApiSdk.Utils;
using NokoWebApiSdk.Utils.Net;

namespace NokoWebApi.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    [HttpGet("message")]
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