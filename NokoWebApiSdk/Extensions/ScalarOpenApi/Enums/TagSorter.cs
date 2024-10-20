using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum TagSorter
{
    Alpha
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public sealed class TagSorterValues
{
    public const string Alpha = "alpha";

    public static TagSorter ParseCode(string code)
    {
        return (NokoWebTransformText.ToSnakeCase(code)) switch
        {
            Alpha => TagSorter.Alpha,
            _ => throw new FormatException($"Invalid tag sorter code {code}"),
        };
    }
    
    public static string FromCode(TagSorter code)
    {
        return (code) switch
        {
            TagSorter.Alpha => Alpha,
            _ => throw new Exception($"Unsupported target tag sorter code {code}"),
        };
    }
}

public static class TagSorterExtensions
{
    public static string GetKey(this TagSorter code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this TagSorter code)
    {
        return TagSorterValues.FromCode(code);
    }
}

public class TagSorterSerializeConverter : JsonConverter<TagSorter>
{
    public override TagSorter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TagSorter value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
