using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Schemas;

namespace NokoWebApi.Schemas;

public interface IUserSessionLookupData
{
    public Guid SessionId { get; set; }
    public bool Used { get; set; }
    // public bool Online { get; set; }
    public string IpAddr { get; set; }
    public string UserAgent { get; set; }
    public DateTime Expires { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class UserSessionLookupData(Guid sessionId, bool used, string ipAddr, string userAgent, DateTime expires, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt) : IUserSessionLookupData
{
    public UserSessionLookupData() : this(default, default, "", "", default, default, default, default)
    {
        // do nothing...
    }

    [JsonPropertyName("sessionId")]
    public Guid SessionId { get; set; } = sessionId;
    
    [JsonPropertyName("used")]
    public bool Used { get; set; } = used;
    
    [Required]
    [JsonPropertyName("ipAddr")]
    public string IpAddr { get; set; } = ipAddr;
    
    [Required]
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; } = userAgent;
    
    [JsonPropertyName("expiredAt")]
    public DateTime Expires { get; set; } = expires;
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = createdAt;
    
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = updatedAt;
    
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; set; } = deletedAt;
}

public class UserSessionLookupManyMessageBody : MessageBody<UserSessionLookupData[]>;
