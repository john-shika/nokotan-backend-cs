using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiTargets
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

public static class ScalarOpenApiTargetValues
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

    public static ScalarOpenApiTargets ParseCode(string code)
    {
        return (NokoTransformText.ToCamelCase(code)) switch {
            C => ScalarOpenApiTargets.C,
            Clojure => ScalarOpenApiTargets.Clojure,
            CSharp => ScalarOpenApiTargets.CSharp,
            Go => ScalarOpenApiTargets.Go,
            Http => ScalarOpenApiTargets.Http,
            Java => ScalarOpenApiTargets.Java,
            JavaScript => ScalarOpenApiTargets.JavaScript,
            Kotlin => ScalarOpenApiTargets.Kotlin,
            Node => ScalarOpenApiTargets.Node,
            ObjC => ScalarOpenApiTargets.ObjC,
            OCaml => ScalarOpenApiTargets.OCaml,
            Php => ScalarOpenApiTargets.Php,
            PowerShell => ScalarOpenApiTargets.PowerShell,
            Python => ScalarOpenApiTargets.Python,
            R => ScalarOpenApiTargets.R,
            Ruby => ScalarOpenApiTargets.Ruby,
            Shell => ScalarOpenApiTargets.Shell,
            Swift => ScalarOpenApiTargets.Swift,
            _ => throw new FormatException($"Invalid target code {code}"),
        };
    }

    public static string FromCode(ScalarOpenApiTargets code)
    {
        return (code) switch
        {
            ScalarOpenApiTargets.C => C,
            ScalarOpenApiTargets.Clojure => Clojure,
            ScalarOpenApiTargets.CSharp => CSharp,
            ScalarOpenApiTargets.Go => Go,
            ScalarOpenApiTargets.Http => Http,
            ScalarOpenApiTargets.Java => Java,
            ScalarOpenApiTargets.JavaScript => JavaScript,
            ScalarOpenApiTargets.Kotlin => Kotlin,
            ScalarOpenApiTargets.Node => Node,
            ScalarOpenApiTargets.ObjC => ObjC,
            ScalarOpenApiTargets.OCaml => OCaml,
            ScalarOpenApiTargets.Php => Php,
            ScalarOpenApiTargets.PowerShell => PowerShell,
            ScalarOpenApiTargets.Python => Python,
            ScalarOpenApiTargets.R => R,
            ScalarOpenApiTargets.Ruby => Ruby,
            ScalarOpenApiTargets.Shell => Shell,
            ScalarOpenApiTargets.Swift => Swift,
            _ => throw new Exception($"Unsupported target code {code}"),
        };
    }
}

public static class ScalarOpenApiTargetExtensions
{
    public static string GetKey(this ScalarOpenApiTargets code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiTargets code)
    {
        return ScalarOpenApiTargetValues.FromCode(code);
    }
}

public class ScalarOpenApiTargetSerializeConverter : JsonConverter<ScalarOpenApiTargets>
{
    public override ScalarOpenApiTargets Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiTargets value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
