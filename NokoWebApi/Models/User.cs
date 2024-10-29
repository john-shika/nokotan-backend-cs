using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Models;

namespace NokoWebApi.Models;

[Table("users")]
public class User(string? fullname, string username, string password, string? email, string? phone, DateTime? birthday, string? gender, string? country, string? city, string? state, string? postCode, bool admin) : BaseModel
{
    public User() : this(null, "", "", null, null, null, null, null, null, null, null, false) 
    {
        // do nothing...
    }

    [Column("fullname")]
    [JsonPropertyName("fullname")]
    public string? Fullname { get; set; } = fullname;
    
    [Required]
    [UniqueKey]
    [Column("username")]
    [JsonPropertyName("username")]
    public string Username { get; set; } = username;

    [Required]
    [Column("password")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = password;
    
    [UniqueKey]
    [Column("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; } = email;
    
    [UniqueKey]
    [Column("phone")]
    [JsonPropertyName("phone")]
    public string? Phone { get; set; } = phone;
    
    [Column("birthdate")]
    [JsonPropertyName("birthdate")]
    public DateTime? Birthdate { get; set; } = birthday;
    
    [Column("gender")]
    [JsonPropertyName("gender")]
    public string? Gender { get; set; } = gender;
    
    [Column("country")]
    [JsonPropertyName("country")]
    public string? Country { get; set; } = country;
    
    [Column("city")]
    [JsonPropertyName("city")]
    public string? City { get; set; } = city;
    
    [Column("state")]
    [JsonPropertyName("state")]
    public string? State { get; set; } = state;
    
    [Column("postcode")]
    [JsonPropertyName("postcode")]
    public string? Postcode { get; set; } = postCode;
    
    [Required]
    [Column("admin")]
    [JsonPropertyName("admin")]
    public bool Admin { get; set; } = admin;
}