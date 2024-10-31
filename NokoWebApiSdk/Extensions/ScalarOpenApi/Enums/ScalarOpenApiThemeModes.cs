using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiThemeModes
{
    Light,
    Dark
}

public static class ScalarOpenApiThemeModeValues
{
    public const string Dark = "dark";
    public const string Light = "light";

    public static ScalarOpenApiThemeModes ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch
        {
            Dark => ScalarOpenApiThemeModes.Dark,
            Light => ScalarOpenApiThemeModes.Light,
            _ => throw new FormatException($"Invalid theme mode code {code}"),
        };
    }
    
    public static string FromCode(ScalarOpenApiThemeModes code)
    {
        return (code) switch
        {
            ScalarOpenApiThemeModes.Dark => Dark,
            ScalarOpenApiThemeModes.Light => Light,
            _ => throw new Exception($"Unsupported target theme mode code {code}"),
        };
    }
}

public static class ScalarOpenApiThemeModeExtensions
{
    public static string GetKey(this ScalarOpenApiThemeModes code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiThemeModes code)
    {
        return ScalarOpenApiThemeModeValues.FromCode(code);
    }
}

public class ScalarOpenApiThemeModeSerializeConverter : JsonConverter<ScalarOpenApiThemeModes>
{
    public override ScalarOpenApiThemeModes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiThemeModes value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
