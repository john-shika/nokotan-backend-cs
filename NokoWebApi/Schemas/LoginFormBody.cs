using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApi.Schemas;

public class LoginFormBody(string username, string password)
{
    public LoginFormBody() : this("", "")
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; } = username;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = password;
}