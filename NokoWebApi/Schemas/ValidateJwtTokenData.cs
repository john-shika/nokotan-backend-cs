using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IValidateJwtTokenData
{
    public Guid TokenId { get; set; }
    public Guid SessionId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime Expires { get; set; }
}

public record ValidateJwtTokenData : IValidateJwtTokenData
{
    
    [Required]
    [JsonPropertyName("tokenId")]
    public Guid TokenId { get; set; }
    
    [Required]
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; }
    
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required]
    [JsonPropertyName("role")]
    public string Role { get; set; }
    
    [Required]
    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; }
}

public record ValidateJwtTokenMessageBody : MessageBody<ValidateJwtTokenData>;
