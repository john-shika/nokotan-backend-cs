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

public record MessageBody<T> : IMessageBody<T> 
    where T : class
{
    [JsonPropertyName("statusOk")] 
    public bool StatusOk { get; set; }
    
    [JsonPropertyName("statusCode")] 
    public int StatusCode { get; set; }
    
    [Required]
    [JsonPropertyName("status")] 
    public string Status { get; set; }
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    
    [Required]
    [JsonPropertyName("message")] 
    public string Message { get; set; }
    
    [JsonPropertyName("data")] 
    public T? Data { get; set; }
}