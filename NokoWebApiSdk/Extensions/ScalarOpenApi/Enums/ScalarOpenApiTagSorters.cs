using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiTagSorters
{
    Alpha
}

public static class ScalarOpenApiTagSorterValues
{
    public const string Alpha = "alpha";

    public static ScalarOpenApiTagSorters ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch
        {
            Alpha => ScalarOpenApiTagSorters.Alpha,
            _ => throw new FormatException($"Invalid tag sorter code {code}"),
        };
    }
    
    public static string FromCode(ScalarOpenApiTagSorters code)
    {
        return (code) switch
        {
            ScalarOpenApiTagSorters.Alpha => Alpha,
            _ => throw new Exception($"Unsupported target tag sorter code {code}"),
        };
    }
}

public static class ScalarOpenApiTagSorterExtensions
{
    public static string GetKey(this ScalarOpenApiTagSorters code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiTagSorters code)
    {
        return ScalarOpenApiTagSorterValues.FromCode(code);
    }
}

public class ScalarOpenApiTagSorterSerializeConverter : JsonConverter<ScalarOpenApiTagSorters>
{
    public override ScalarOpenApiTagSorters Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiTagSorters value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
