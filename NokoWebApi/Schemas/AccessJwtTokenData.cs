using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IAccessJwtTokenData
{
    public string AccessToken { get; init; }
}

public record AccessJwtTokenData : IAccessJwtTokenData
{
    [Required]
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; init; }
}

public record AccessJwtTokenMessageBody : MessageBody<AccessJwtTokenData>;
