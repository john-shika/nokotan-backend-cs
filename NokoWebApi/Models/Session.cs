using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Models;

namespace NokoWebApi.Models;

[Table("sessions")]
public record Session : BaseModel
{
    [Required]
    [Column("user_id")]
    [ForeignKey("User")]
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    
    [Required]
    [UniqueKey]
    [Column("token_id")]
    [JsonPropertyName("tokenId")]
    public string TokenId { get; set; }
    
    [UniqueKey]
    [Column("new_token_id")]
    [JsonPropertyName("newTokenId")]
    public string? NewTokenId { get; set; }
    
    [Required]
    [Column("ip_addr")]
    [JsonPropertyName("ipAddr")]
    public string IpAddr { get; set; }
    
    [Required]
    [Column("user_agent")]
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; }
    
    [Required]
    [Column("expired_at")]
    [JsonPropertyName("expiredAt")]
    public DateTime ExpiredAt { get; set; }
}