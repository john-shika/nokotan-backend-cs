using System.Text.RegularExpressions;

namespace NokoWebApiSdk.Cores.Utils;

public static class NokoTransformText
{
    public static string ToTitleCase(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = Regex.Replace(value.Trim(), @"(?<=[a-z])([A-Z])|(?<=[a-zA-Z])([0-9])|(?<=[0-9])([a-zA-Z])", " $1$2$3");
        val = Regex.Replace(val, @"[-_\s]+", " ");
        val = Regex.Replace(val.ToLower(), @"\b\w", m => m.Value.ToUpper());
        return val;
    }

    public static string ToUpperStart(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = value.Trim();
        return Regex.Replace(val, @"\b\w", m => m.Value.ToUpper());
    }

    public static string ToUpperEnd(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = value.Trim();
        return Regex.Replace(val, @"\w\b", m => m.Value.ToUpper());
    }

    public static string ToLowerStart(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = value.Trim();
        return Regex.Replace(val, @"\b\w", m => m.Value.ToLower());
    }

    public static string ToLowerEnd(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = value.Trim();
        return Regex.Replace(val, @"\w\b", m => m.Value.ToLower());
    }

    public static string ToPascalCase(string value)
    {
        var val = ToTitleCase(value).Replace(" ", "");
        return ToUpperStart(val);
    }

    public static string ToCamelCase(string value)
    {
        var val = ToTitleCase(value).Replace(" ", "");
        return ToLowerStart(val);
    }

    private static string ToSnakeCaseRaw(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = Regex.Replace(value.Trim(), @"(?<=[a-z])([A-Z])|(?<=[a-zA-Z])([0-9])|(?<=[0-9])([a-zA-Z])", "_$1$2$3");
        val = Regex.Replace(val, @"[-_\s]+", "_");
        // val = Regex.Replace(val.ToLower(), @"\b\w", m => m.Value.ToUpper());
        return val;
    }

    public static string ToSnakeCase(string value)
    {
        return ToSnakeCaseRaw(value).ToLower();
    }

    public static string ToSnakeCaseUpper(string value)
    {
        return ToSnakeCaseRaw(value).ToUpper();
    }

    private static string ToKebabCaseRaw(string value)
    {
        if (NokoCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var val = Regex.Replace(value.Trim(), @"(?<=[a-z])([A-Z])|(?<=[a-zA-Z])([0-9])|(?<=[0-9])([a-zA-Z])", "-$1$2$3");
        val = Regex.Replace(val, @"[-_\s]+", "-");
        // val = Regex.Replace(val.ToLower(), @"\b\w", m => m.Value.ToUpper());
        return val;
    }

    public static string ToKebabCase(string value)
    {
        return ToKebabCaseRaw(value).ToLower();
    }

    public static string ToKebabCaseUpper(string value)
    {
        return ToKebabCaseRaw(value);
    }
}