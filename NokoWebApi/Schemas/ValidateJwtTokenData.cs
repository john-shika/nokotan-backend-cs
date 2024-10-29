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
    public DateTime IssuedAt { get; set; }
}

public class ValidateJwtTokenData(Guid tokenId, Guid sessionId, string username, string email, string role, DateTime expires, DateTime issuedAt) : IValidateJwtTokenData
{
    public ValidateJwtTokenData() : this(default, default, "", "", "", default, default)
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("tokenId")]
    public Guid TokenId { get; set; } = tokenId;
    
    [Required]
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; } = sessionId;
    
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; } = username;
    
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; } = email;
    
    [Required]
    [JsonPropertyName("role")]
    public string Role { get; set; } = role;
    
    [Required]
    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; } = expires;
    
    [Required]
    [JsonPropertyName("issuedAt")]
    public DateTime IssuedAt { get; set; } = issuedAt;
}

public class ValidateJwtTokenMessageBody : MessageBody<ValidateJwtTokenData>;
