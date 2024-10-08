using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApi.Schemas;

public class LoginFormBody
{
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}