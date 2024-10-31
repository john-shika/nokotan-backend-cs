using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Networking;

public enum NokoHttpMethods
{
    Get,
    Post,
    Put,
    Delete,
    Connect,
    Options,
    Head,
    Trace,
    Patch,
}

public static class NokoHttpMethodValues
{
    public const string Get = "GET";
    public const string Post = "POST";
    public const string Put = "PUT";
    public const string Delete = "DELETE";
    public const string Connect = "CONNECT";
    public const string Options = "OPTIONS";
    public const string Head = "HEAD";
    public const string Trace = "TRACE";
    public const string Patch = "PATCH";
    
    public static NokoHttpMethods ParseCode(string code)
    {
        return NokoTransformText.ToSnakeCaseUpper(code) switch
        {
            Get => NokoHttpMethods.Get,
            Post => NokoHttpMethods.Post,
            Put => NokoHttpMethods.Put,
            Delete => NokoHttpMethods.Delete,
            Connect => NokoHttpMethods.Connect,
            Options => NokoHttpMethods.Options,
            Head => NokoHttpMethods.Head,
            Trace => NokoHttpMethods.Trace,
            Patch => NokoHttpMethods.Patch,
            _ => throw new FormatException($"Invalid http method {code}"),
        };
    }

    public static string FromCode(NokoHttpMethods code)
    {
        return code switch
        {
            NokoHttpMethods.Get => Get,
            NokoHttpMethods.Post => Post,
            NokoHttpMethods.Put => Put,
            NokoHttpMethods.Delete => Delete,
            NokoHttpMethods.Connect => Connect,
            NokoHttpMethods.Options => Options,
            NokoHttpMethods.Head => Head,
            NokoHttpMethods.Trace => Trace,
            NokoHttpMethods.Patch => Patch,
            _ => throw new Exception($"Unsupported http method {code}"),
        };
    }
}

public static class NokoHttpMethodExtensions
{
    public static string GetKey(this NokoHttpMethods code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this NokoHttpMethods code)
    {
        return NokoHttpMethodValues.FromCode(code);
    }
}

public class NokoJsonHttpMethodSerializerConverter : JsonConverter<NokoHttpMethods>
{
    public override NokoHttpMethods Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NokoHttpMethods value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
