using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Schemas;

public interface IMessageBody<T>
{
    public bool StatusOk { get; set; }
    public int StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public T? Data { get; set; }
}

public class MessageBody<T>(bool statusOk, int statusCode, string status, DateTime timestamp, string message, T? data) : IMessageBody<T> 
    where T : class
{
    public MessageBody() : this(true, 200, "OK", DateTime.UtcNow, "Successfully Send Message Body.", default) 
    {
        // do nothing...
    }

    [JsonPropertyName("statusOk")] 
    public bool StatusOk { get; set; } = statusOk;
    
    [JsonPropertyName("statusCode")] 
    public int StatusCode { get; set; } = statusCode;

    [Required]
    [JsonPropertyName("status")]
    public string Status { get; set; } = status;
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = timestamp;

    [Required]
    [JsonPropertyName("message")]
    public string Message { get; set; } = message;
    
    [JsonPropertyName("data")] 
    public T? Data { get; set; } = data;
}