using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Schemas;

public class JwtSettings(string secretKey, string issuer, string audience)
{
    public JwtSettings() : this("", "", "") 
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("secretKey")] 
    public string SecretKey { get; set; } = secretKey;

    [Required]
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = issuer;
    
    [JsonPropertyName("audience")]
    public string Audience { get; set; } = audience;
}