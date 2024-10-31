using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Cores.Authentication;

public class NokoWebToken(Guid id, Guid sessionId, string? subject, string? user, string? role, string? email, string? phone, string? issuer, string[] audiences, DateTime? notBefore, DateTime expires, DateTime issuedAt)
{
    public NokoWebToken() : this(default, default, "", "", "", null, null, null, [], default, default, default)
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = id; // web token identity
    
    [Required]
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; } = sessionId;
    
    [Required]
    [JsonPropertyName("subject")]
    public string? Subject { get; set; } = subject;
    
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
    [JsonPropertyName("notBefore")]
    public DateTime? NotBefore { get; set; } = notBefore;
    
    [Required]
    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; } = expires;
    
    [Required]
    [JsonPropertyName("issuedAt")]
    public DateTime IssuedAt { get; set; } = issuedAt; // web token creation time 
}