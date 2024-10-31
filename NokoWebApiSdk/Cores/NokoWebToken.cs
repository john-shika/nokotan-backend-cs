using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Cores;

public class NokoWebToken(Guid id, Guid sessionId, string? user, string? role, string? email, string? phone, string? issuer, string[] audiences, DateTime expires, DateTime issuedAt)
{
    public NokoWebToken() : this(default, default, "", "", null, null, null, [], default, default)
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = id;
    
    [Required]
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; } = sessionId;
    
    [Required]
    [JsonPropertyName("user")]
    public string? User { get; set; } = user; // can get value from session model
    
    [Required]
    [JsonPropertyName("role")]
    public string? Role { get; set; } = role; // can get value from session model
    
    [JsonPropertyName("email")]
    public string? Email { get; set; } = email; // can get value from session model
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; } = phone; // can get value from session model
    
    [JsonPropertyName("issuer")]
    public string? Issuer { get; set; } = issuer; // optional
    
    [JsonPropertyName("audiences")]
    public string[] Audiences { get; set; } = audiences; // optional
    
    [Required]
    [JsonPropertyName("Expires")]
    public DateTime Expires { get; set; } = expires;
    
    [Required]
    [JsonPropertyName("issuedAt")]
    public DateTime IssuedAt { get; set; } = issuedAt; // web token creation time 
}