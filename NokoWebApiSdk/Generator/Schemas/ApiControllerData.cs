using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Net;

namespace NokoWebApiSdk.Generator.Schemas;

public class MemberDataBase(string name, string @namespace, string[] genericTypes) 
{
    public MemberDataBase() : this("", "", [])
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

public class ApiControllerParameterData(int index, string name, string @namespace, string[] genericTypes)
    : MemberDataBase(name, @namespace, genericTypes)
{
    public ApiControllerParameterData() : this(0, "", "", [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("index")]
    public int Index { get; set; } = index;
}

public class ApiControllerEndpointProduceData(int statusCode, string name, string @namespace, string[] genericTypes)
    : MemberDataBase(name, @namespace, genericTypes)
{
    public ApiControllerEndpointProduceData() : this(0, "", "", [])
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; } = statusCode;
}

public class ApiControllerEndpointData(string name, string path, string summary, string description, string[] tags, NokoHttpMethods[] methods, ApiControllerEndpointProduceData[] produces, bool authorization)
{
    public ApiControllerEndpointData() : this("", "", "", "", [], [], [], false)
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
    [Required]
    [JsonPropertyName("path")]
    public string Path { get; set; } = path;
    
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
    [JsonPropertyName("methods")]
    public NokoHttpMethods[] Methods { get; set; } = methods;
    
    [Required]
    [JsonPropertyName("produces")]
    public ApiControllerEndpointProduceData[] Produces { get; set; } = produces;
    
    [Required]
    [JsonPropertyName("authorization")]
    public bool Authorization { get; set; } = authorization;
}

public class ApiControllerData(string name, string @namespace, string path, ApiControllerParameterData[] parameters, ApiControllerEndpointData[] endpoints)
{
    public ApiControllerData() : this("", "", "", [], [])
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
    [JsonPropertyName("path")]
    public string Path { get; set; } = path;
    
    [Required]
    [JsonPropertyName("parameters")]
    public ApiControllerParameterData[] Parameters { get; set; } = parameters;
    
    [Required]
    [JsonPropertyName("endpoints")]
    public ApiControllerEndpointData[] Endpoints { get; set; } = endpoints;
}
