using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiExtra.Schemas;

namespace NokoWebApi.Schemas;

public interface IUserSessionLookupData
{
    public Guid SessionId { get; init; }
    public bool Used { get; init; }
    public bool Online { get; init; }
    public string IpAddr { get; init; }
    public string UserAgent { get; init; }
    public DateTime ExpiredAt { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime? DeletedAt { get; init; }
}

public record UserSessionLookupData : IUserSessionLookupData
{
    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; init; }
    
    [JsonPropertyName("used")]
    public bool Used { get; init; }
    
    [JsonPropertyName("online")]
    public bool Online { get; init; }
    
    [Required]
    [JsonPropertyName("ipAddr")]
    public string IpAddr { get; init; }
    
    [Required]
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; init; }
    
    [JsonPropertyName("expiredAt")]
    public DateTime ExpiredAt { get; init; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }
    
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
    
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; init; }
}

public record UserSessionLookupDataMany
{
    public List<UserSessionLookupData> Data { get; init; }
}

public record UserSessionLookupManyMessageBody : MessageBody<UserSessionLookupDataMany>;
