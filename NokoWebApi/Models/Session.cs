using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Models;

namespace NokoWebApi.Models;

[Table("sessions")]
public class Session(Guid userId, string tokenId, string? refreshTokenId, string ipAddr, string userAgent, DateTime expires) : BaseModel
{
    public Session() : this(default, "", null, "", "", default)
    {
        // do nothing...
    }

    [Required]
    [Column("user_id")]
    [ForeignKey("User")]
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; } = userId;
    
    [Required]
    [UniqueKey]
    [StringLength(128)]
    [Column("token_id")]
    [JsonPropertyName("tokenId")]
    public string TokenId { get; set; } = tokenId;
    
    [UniqueKey]
    [StringLength(128)]
    [Column("refresh_token_id")]
    [JsonPropertyName("refreshTokenId")]
    public string? RefreshTokenId { get; set; } = refreshTokenId;
    
    [Required]
    [StringLength(64)]
    [Column("ip_addr")]
    [JsonPropertyName("ipAddr")]
    public string IpAddr { get; set; } = ipAddr;
    
    [Required]
    [StringLength(256)]
    [Column("user_agent")]
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; } = userAgent;
    
    [Required]
    [Column("expires")]
    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; } = expires;
}