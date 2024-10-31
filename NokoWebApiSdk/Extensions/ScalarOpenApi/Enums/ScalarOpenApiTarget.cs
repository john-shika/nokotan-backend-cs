using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiTarget
{
    C,
    Clojure,
    CSharp,
    Go,
    Http,
    Java,
    JavaScript,
    Kotlin,
    Node,
    ObjC,
    OCaml,
    Php,
    PowerShell,
    Python,
    R,
    Ruby,
    Shell,
    Swift,
}

public sealed class ScalarOpenApiTargetValue
{
    public const string C = "c";
    public const string Clojure = "clojure";
    public const string CSharp = "csharp";
    public const string Go = "go";
    public const string Http = "http";
    public const string Java = "java";
    public const string JavaScript = "javascript";
    public const string Kotlin = "kotlin";
    public const string Node = "node";
    public const string ObjC = "objc";
    public const string OCaml = "ocaml";
    public const string Php = "php";
    public const string PowerShell = "powershell";
    public const string Python = "python";
    public const string R = "r";
    public const string Ruby = "ruby";
    public const string Shell = "shell";
    public const string Swift = "swift";

    public static ScalarOpenApiTarget ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch {
            C => ScalarOpenApiTarget.C,
            Clojure => ScalarOpenApiTarget.Clojure,
            CSharp => ScalarOpenApiTarget.CSharp,
            Go => ScalarOpenApiTarget.Go,
            Http => ScalarOpenApiTarget.Http,
            Java => ScalarOpenApiTarget.Java,
            JavaScript => ScalarOpenApiTarget.JavaScript,
            Kotlin => ScalarOpenApiTarget.Kotlin,
            Node => ScalarOpenApiTarget.Node,
            ObjC => ScalarOpenApiTarget.ObjC,
            OCaml => ScalarOpenApiTarget.OCaml,
            Php => ScalarOpenApiTarget.Php,
            PowerShell => ScalarOpenApiTarget.PowerShell,
            Python => ScalarOpenApiTarget.Python,
            R => ScalarOpenApiTarget.R,
            Ruby => ScalarOpenApiTarget.Ruby,
            Shell => ScalarOpenApiTarget.Shell,
            Swift => ScalarOpenApiTarget.Swift,
            _ => throw new FormatException($"Invalid target code {code}"),
        };
    }

    public static string FromCode(ScalarOpenApiTarget code)
    {
        return (code) switch
        {
            ScalarOpenApiTarget.C => C,
            ScalarOpenApiTarget.Clojure => Clojure,
            ScalarOpenApiTarget.CSharp => CSharp,
            ScalarOpenApiTarget.Go => Go,
            ScalarOpenApiTarget.Http => Http,
            ScalarOpenApiTarget.Java => Java,
            ScalarOpenApiTarget.JavaScript => JavaScript,
            ScalarOpenApiTarget.Kotlin => Kotlin,
            ScalarOpenApiTarget.Node => Node,
            ScalarOpenApiTarget.ObjC => ObjC,
            ScalarOpenApiTarget.OCaml => OCaml,
            ScalarOpenApiTarget.Php => Php,
            ScalarOpenApiTarget.PowerShell => PowerShell,
            ScalarOpenApiTarget.Python => Python,
            ScalarOpenApiTarget.R => R,
            ScalarOpenApiTarget.Ruby => Ruby,
            ScalarOpenApiTarget.Shell => Shell,
            ScalarOpenApiTarget.Swift => Swift,
            _ => throw new Exception($"Unsupported target code {code}"),
        };
    }
}

public static class ScalarOpenApiTargetExtensions
{
    public static string GetKey(this ScalarOpenApiTarget code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiTarget code)
    {
        return ScalarOpenApiTargetValue.FromCode(code);
    }
}

public class ScalarOpenApiTargetSerializeConverter : JsonConverter<ScalarOpenApiTarget>
{
    public override ScalarOpenApiTarget Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiTarget value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
