using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Net;

public enum NokoHttpMethod
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

public sealed class NokoHttpMethodValue
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
    
    public static NokoHttpMethod ParseCode(string code)
    {
        return NokoTransformText.ToSnakeCaseUpper(code) switch
        {
            Get => NokoHttpMethod.Get,
            Post => NokoHttpMethod.Post,
            Put => NokoHttpMethod.Put,
            Delete => NokoHttpMethod.Delete,
            Connect => NokoHttpMethod.Connect,
            Options => NokoHttpMethod.Options,
            Head => NokoHttpMethod.Head,
            Trace => NokoHttpMethod.Trace,
            Patch => NokoHttpMethod.Patch,
            _ => throw new FormatException($"Invalid http method {code}"),
        };
    }

    public static string FromCode(NokoHttpMethod code)
    {
        return code switch
        {
            NokoHttpMethod.Get => Get,
            NokoHttpMethod.Post => Post,
            NokoHttpMethod.Put => Put,
            NokoHttpMethod.Delete => Delete,
            NokoHttpMethod.Connect => Connect,
            NokoHttpMethod.Options => Options,
            NokoHttpMethod.Head => Head,
            NokoHttpMethod.Trace => Trace,
            NokoHttpMethod.Patch => Patch,
            _ => throw new Exception($"Unsupported http method {code}"),
        };
    }
}

public static class NokoHttpMethodExtensions
{
    public static string GetKey(this NokoHttpMethod code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this NokoHttpMethod code)
    {
        return NokoHttpMethodValue.FromCode(code);
    }
}

public class NokoJsonHttpMethodSerializerConverter : JsonConverter<NokoHttpMethod>
{
    public override NokoHttpMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NokoHttpMethod value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
