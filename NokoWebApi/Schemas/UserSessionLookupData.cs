using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IUserSessionLookupData
{
    public Guid SessionId { get; set; }
    public bool Used { get; set; }
    // public bool Online { get; set; }
    public string IpAddr { get; set; }
    public string UserAgent { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public record UserSessionLookupData : IUserSessionLookupData
{
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; }
    
    [JsonPropertyName("used")]
    public bool Used { get; set; }
    
    // [JsonPropertyName("online")]
    // public bool Online { get; set; }
    
    [Required]
    [JsonPropertyName("ipAddr")]
    public string IpAddr { get; set; }
    
    [Required]
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; }
    
    [JsonPropertyName("expiredAt")]
    public DateTime ExpiredAt { get; set; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; set; }
}

public record UserSessionLookupManyMessageBody : MessageBody<UserSessionLookupData[]>;
