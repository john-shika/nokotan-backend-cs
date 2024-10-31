using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiThemes
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

public static class ScalarOpenApiThemeValues
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

    public static ScalarOpenApiThemes ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch
        {
            None => ScalarOpenApiThemes.None,
            Alternate => ScalarOpenApiThemes.Alternate,
            Default => ScalarOpenApiThemes.Default,
            Moon => ScalarOpenApiThemes.Moon,
            Purple => ScalarOpenApiThemes.Purple,
            Solarized => ScalarOpenApiThemes.Solarized,
            BluePlanet => ScalarOpenApiThemes.BluePlanet,
            Saturn => ScalarOpenApiThemes.Saturn,
            Kepler => ScalarOpenApiThemes.Kepler,
            Mars => ScalarOpenApiThemes.Mars,
            DeepSpace => ScalarOpenApiThemes.DeepSpace,
            _ => throw new FormatException($"Invalid theme code {code}"),
        };
    }
    
    public static string FromCode(ScalarOpenApiThemes code)
    {
        return (code) switch
        {
            ScalarOpenApiThemes.None => None,
            ScalarOpenApiThemes.Alternate => Alternate,
            ScalarOpenApiThemes.Default => Default,
            ScalarOpenApiThemes.Moon => Moon,
            ScalarOpenApiThemes.Purple => Purple,
            ScalarOpenApiThemes.Solarized => Solarized,
            ScalarOpenApiThemes.BluePlanet => BluePlanet,
            ScalarOpenApiThemes.Saturn => Saturn,
            ScalarOpenApiThemes.Kepler => Kepler,
            ScalarOpenApiThemes.Mars => Mars,
            ScalarOpenApiThemes.DeepSpace => DeepSpace,
            _ => throw new Exception($"Unsupported target code {code}"),
        };
    }
}

public static class ScalarOpenApiThemeExtensions
{
    public static string GetKey(this ScalarOpenApiThemes code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiThemes code)
    {
        return ScalarOpenApiThemeValues.FromCode(code);
    }
}

public class ScalarOpenApiThemeSerializeConverter : JsonConverter<ScalarOpenApiThemes>
{
    public override ScalarOpenApiThemes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiThemes value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
