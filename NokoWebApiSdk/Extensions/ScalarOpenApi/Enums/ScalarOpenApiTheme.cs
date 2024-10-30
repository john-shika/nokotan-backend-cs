using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiTheme
{
    None,
    Alternate,
    Default,
    Moon,
    Purple,
    Solarized,
    BluePlanet,
    Saturn,
    Kepler,
    Mars,
    DeepSpace,
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public sealed class ScalarOpenApiThemeValues
{
    public const string None = "none";
    public const string Alternate = "alternate";
    public const string Default = "default";
    public const string Moon = "moon";
    public const string Purple = "purple";
    public const string Solarized = "solarized";
    public const string BluePlanet = "bluePlanet";
    public const string Saturn = "saturn";
    public const string Kepler = "kepler";
    public const string Mars = "mars";
    public const string DeepSpace = "deepSpace";

    public static ScalarOpenApiTheme ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch
        {
            None => ScalarOpenApiTheme.None,
            Alternate => ScalarOpenApiTheme.Alternate,
            Default => ScalarOpenApiTheme.Default,
            Moon => ScalarOpenApiTheme.Moon,
            Purple => ScalarOpenApiTheme.Purple,
            Solarized => ScalarOpenApiTheme.Solarized,
            BluePlanet => ScalarOpenApiTheme.BluePlanet,
            Saturn => ScalarOpenApiTheme.Saturn,
            Kepler => ScalarOpenApiTheme.Kepler,
            Mars => ScalarOpenApiTheme.Mars,
            DeepSpace => ScalarOpenApiTheme.DeepSpace,
            _ => throw new FormatException($"Invalid theme code {code}"),
        };
    }
    
    public static string FromCode(ScalarOpenApiTheme code)
    {
        return (code) switch
        {
            ScalarOpenApiTheme.None => None,
            ScalarOpenApiTheme.Alternate => Alternate,
            ScalarOpenApiTheme.Default => Default,
            ScalarOpenApiTheme.Moon => Moon,
            ScalarOpenApiTheme.Purple => Purple,
            ScalarOpenApiTheme.Solarized => Solarized,
            ScalarOpenApiTheme.BluePlanet => BluePlanet,
            ScalarOpenApiTheme.Saturn => Saturn,
            ScalarOpenApiTheme.Kepler => Kepler,
            ScalarOpenApiTheme.Mars => Mars,
            ScalarOpenApiTheme.DeepSpace => DeepSpace,
            _ => throw new Exception($"Unsupported target code {code}"),
        };
    }
}

public static class ScalarOpenApiThemeExtensions
{
    public static string GetKey(this ScalarOpenApiTheme code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiTheme code)
    {
        return ScalarOpenApiThemeValues.FromCode(code);
    }
}

public class ScalarOpenApiThemeSerializeConverter : JsonConverter<ScalarOpenApiTheme>
{
    public override ScalarOpenApiTheme Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiTheme value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
