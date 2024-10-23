using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Schemas;

public class JwtSettings
{
    [JsonPropertyName("secretKey")]
    public string SecretKey { get; set; }
    
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; }
    
    [JsonPropertyName("audience")]
    public string Audience { get; set; }
}