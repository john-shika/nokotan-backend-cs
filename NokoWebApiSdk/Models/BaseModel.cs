using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Models;

public interface IBaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public abstract record BaseModel : IBaseModel
{
    [Key]
    [Column("id")]
    [JsonPropertyName("uuid")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("created_at")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    [Column("updated_at")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    [Column("deleted_at")]
    [JsonPropertyName("deletedAt")]
    public DateTime? DeletedAt { get; set; }
}