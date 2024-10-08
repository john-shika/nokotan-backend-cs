using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Models;

namespace NokoWebApi.Models;

[Table("users")]
public record User : BaseModel
{
    [Column("fullname")]
    [JsonPropertyName("fullname")]
    public string? Fullname { get; set; }
    
    [Required]
    [UniqueKey]
    [Column("username")]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [Required]
    [Column("password")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [UniqueKey]
    [Column("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [UniqueKey]
    [Column("phone")]
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    
    [Column("birthdate")]
    [JsonPropertyName("birthdate")]
    public DateTime? Birthdate { get; set; }
    
    [Column("gender")]
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }
    
    [Column("country")]
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    
    [Column("city")]
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    [Column("state")]
    [JsonPropertyName("state")]
    public string? State { get; set; }
    
    [Column("postcode")]
    [JsonPropertyName("postcode")]
    public string? Postcode { get; set; }
    
    [Required]
    [Column("admin")]
    [JsonPropertyName("admin")]
    public bool Admin { get; set; }
}