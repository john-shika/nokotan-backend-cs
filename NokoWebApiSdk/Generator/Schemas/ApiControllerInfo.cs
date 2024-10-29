using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Generator.Schemas;

public class ApiControllerParameterInfo(string name, string @namespace, string[] genericTypes)
{
    public ApiControllerParameterInfo() : this("", "", [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [Required]
    [JsonPropertyName("namespace")]
    public string Namespace { get; set; } = @namespace;
    
    [Required]
    [JsonPropertyName("genericTypes")]
    public string[] GenericTypes { get; set; } = genericTypes;
}

public class ApiControllerProduceInfo(string name, string @namespace, string[] genericTypes)
{
    public ApiControllerProduceInfo() : this("", "", [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [Required]
    [JsonPropertyName("namespace")]
    public string Namespace { get; set; } = @namespace;
    
    [Required]
    [JsonPropertyName("genericTypes")]
    public string[] GenericTypes { get; set; } = genericTypes;
}

public class ApiControllerMethodInfo(string name, string method, string summary, string description, string[] tags, ApiControllerProduceInfo[] produces)
{
    public ApiControllerMethodInfo() : this("", "", "", "", [], [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [Required]
    [JsonPropertyName("method")]
    public string Method { get; set; } = method;
    
    [Required]
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = summary;
    
    [Required]
    [JsonPropertyName("description")]
    public string Description { get; set; } = description;
    
    [Required]
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = tags;
    
    [Required]
    [JsonPropertyName("produces")]
    public ApiControllerProduceInfo[] Produces { get; set; } = produces;
}

public class ApiControllerInfo(string name, string @namespace, ApiControllerParameterInfo[] parameters, ApiControllerMethodInfo[] methods)
{
    public ApiControllerInfo() : this("", "", [], [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [Required]
    [JsonPropertyName("namespace")]
    public string Namespace { get; set; } = @namespace;

    [Required]
    [JsonPropertyName("parameters")]
    public ApiControllerParameterInfo[] Parameters { get; set; } = parameters;

    [Required]
    [JsonPropertyName("methods")]
    public ApiControllerMethodInfo[] Methods { get; set; } = methods;
    
    
}