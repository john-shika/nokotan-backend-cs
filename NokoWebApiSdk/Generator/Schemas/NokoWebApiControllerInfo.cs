using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace NokoWebApiSdk.Generator.Schemas;

public class NokoWebApiControllerBaseInfo(string name, string @namespace, string[] genericTypes) 
{
    public NokoWebApiControllerBaseInfo() : this("", "", [])
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

public class NokoWebApiControllerParameterInfo(string name, string @namespace, string[] genericTypes) : NokoWebApiControllerBaseInfo(name, @namespace, genericTypes);

public class NokoWebApiControllerProduceInfo(string name, string @namespace, string[] genericTypes) : NokoWebApiControllerBaseInfo(name, @namespace, genericTypes);

public class NokoWebApiControllerMethodInfo(string name, string summary, string description, string[] tags, string[] httpMethods, NokoWebApiControllerProduceInfo[] produces, bool authorization)
{
    public NokoWebApiControllerMethodInfo() : this("", "", "", [], [], [], false)
    {
        // do nothing...
    }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
    
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
    [JsonPropertyName("httpMethods")]
    public string[] HttpHttpMethods { get; set; } = httpMethods;
    
    [Required]
    [JsonPropertyName("produces")]
    public NokoWebApiControllerProduceInfo[] Produces { get; set; } = produces;
    
    [Required]
    [JsonPropertyName("authorization")]
    public bool Authorization { get; set; } = authorization;
}

public class NokoWebApiControllerInfo(string name, string @namespace, NokoWebApiControllerParameterInfo[] parameters, NokoWebApiControllerMethodInfo[] methods)
{
    public NokoWebApiControllerInfo() : this("", "", [], [])
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
    public NokoWebApiControllerParameterInfo[] Parameters { get; set; } = parameters;

    [Required]
    [JsonPropertyName("methods")]
    public NokoWebApiControllerMethodInfo[] Methods { get; set; } = methods;
}
