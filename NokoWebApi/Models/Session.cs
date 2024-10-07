using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NokoWebApiExtra.Models;

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
    [Column("token")]
    [JsonPropertyName("token")]
    public string Token { get; set; }
    
    [Column("new_token")]
    [JsonPropertyName("newToken")]
    public string? NewToken { get; set; }
    
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