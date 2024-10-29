using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IAccessJwtTokenData
{
    public string AccessToken { get; set; }
}

public class AccessJwtTokenData(string accessToken) : IAccessJwtTokenData
{
    public AccessJwtTokenData() : this("")
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = accessToken;
}

public class AccessJwtTokenMessageBody : MessageBody<AccessJwtTokenData>;
