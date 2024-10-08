using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Schemas;

public interface IMessageBody<T>
{
    public bool StatusOk { get; init; }
    public int StatusCode { get; init; }
    public string Status { get; init; }
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
    public T? Data { get; init; }
}

public record MessageBody<T> : IMessageBody<T> 
    where T : class
{
    [JsonPropertyName("statusOk")] 
    public bool StatusOk { get; init; }
    
    [JsonPropertyName("statusCode")] 
    public int StatusCode { get; init; }
    
    [Required]
    [JsonPropertyName("status")] 
    public string Status { get; init; }
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; init; }
    
    [Required]
    [JsonPropertyName("message")] 
    public string Message { get; init; }
    
    [JsonPropertyName("data")] 
    public T? Data { get; init; }
}