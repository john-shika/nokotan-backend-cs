using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Net;

namespace NokoWebApiSdk.Schemas;

public interface IMessageBody<T>
{
    public bool StatusOk { get; set; }
    public NokoHttpStatusCode StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public T? Data { get; set; }
}

public class MessageBody<T>(bool statusOk, NokoHttpStatusCode statusCode, string status, DateTime timestamp, string message, T? data) : IMessageBody<T> 
    where T : class
{
    public MessageBody() : this(false, 0, "", default, "", null) 
    {
        // do nothing...
    }

    [JsonPropertyName("statusOk")] 
    public bool StatusOk { get; set; } = statusOk;
    
    [JsonPropertyName("statusCode")] 
    public NokoHttpStatusCode StatusCode { get; set; } = statusCode;

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