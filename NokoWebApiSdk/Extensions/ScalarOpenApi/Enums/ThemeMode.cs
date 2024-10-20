using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ThemeMode
{
    Light,
    Dark
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public sealed class ThemeModeValues
{
    public const string Dark = "dark";
    public const string Light = "light";

    public static ThemeMode ParseCode(string code)
    {
        return (NokoWebTransformText.ToSnakeCase(code)) switch
        {
            Dark => ThemeMode.Dark,
            Light => ThemeMode.Light,
            _ => throw new FormatException($"Invalid theme mode code {code}"),
        };
    }
    
    public static string FromCode(ThemeMode code)
    {
        return (code) switch
        {
            ThemeMode.Dark => Dark,
            ThemeMode.Light => Light,
            _ => throw new Exception($"Unsupported target theme mode code {code}"),
        };
    }
}

public static class ThemeModeExtensions
{
    public static string GetKey(this ThemeMode code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ThemeMode code)
    {
        return ThemeModeValues.FromCode(code);
    }
}

public class ThemeModeSerializeConverter : JsonConverter<ThemeMode>
{
    public override ThemeMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ThemeMode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
