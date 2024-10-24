using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IAccessJwtTokenData
{
    public string AccessToken { get; set; }
}

public class AccessJwtTokenData : IAccessJwtTokenData
{
    [Required]
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
}

public class AccessJwtTokenMessageBody : MessageBody<AccessJwtTokenData>;
