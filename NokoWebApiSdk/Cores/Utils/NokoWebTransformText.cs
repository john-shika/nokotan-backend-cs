using System.Globalization;
using System.Text.RegularExpressions;

namespace NokoWebApiSdk.Cores.Utils;

public static class NokoWebTransformText
{
    public static string ToTitleCase(string value)
    {
        if (NokoWebCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        //var temp = Regex.Replace(value.Trim(), @"(?<=[a-zA-Z])([A-Z]([a-z]+)?|[0-9]+)|[-_\s]+", " $1");
        var temp = Regex.Replace(value.Trim(), @"(?<=\w)([A-Z])", " $1");
        temp = Regex.Replace(temp, @"^([-_\s]+)?([0-9]+)", "$1 ");
        temp = Regex.Replace(temp, @"(?<=\w)([0-9]+)", " $1 ");
        temp = Regex.Replace(temp, @"[-_\s]+", " ");
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(temp);
    }

    public static string ToStartCharUpper(string value)
    {
        return value.Length switch
        {
            0 => string.Empty,
            1 => string.Empty + char.ToUpper(value[0]),
            _ => string.Empty + char.ToUpper(value[0]) + value[1..],
        };
    }
    
    public static string ToStartCharLower(string value)
    {
        return value.Length switch
        {
            0 => string.Empty,
            1 => string.Empty + char.ToLower(value[0]),
            _ => string.Empty + char.ToLower(value[0]) + value[1..],
        };
    }

    public static string ToPascalCase(string value)
    {
        var temp = ToTitleCase(value);
        temp = temp.Replace(" ", "");
        return ToStartCharUpper(temp);
    }
    
    public static string ToCamelCase(string value)
    {
        var temp = ToTitleCase(value);
        temp = temp.Replace(" ", "");
        return ToStartCharLower(temp);
    }
    
    private static string ToSnakeCaseRaw(string value)
    {
        if (NokoWebCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var temp = Regex.Replace(value.Trim(), @"(?<=\w)([A-Z])", "_$1");
        temp = Regex.Replace(temp, @"^([-_\s]+)?([0-9]+)", "$1_");
        temp = Regex.Replace(temp, @"(?<=\w)([0-9]+)", "_$1_");
        temp = Regex.Replace(temp, @"[-_\s]+", "_");
        return temp;
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
        if (NokoWebCommonMod.IsNoneOrEmptyWhiteSpace(value)) return string.Empty;
        var temp = Regex.Replace(value.Trim(), @"(?<=\w)([A-Z])", "-$1");
        temp = Regex.Replace(temp, @"^([-_\s]+)?([0-9]+)", "$1-");
        temp = Regex.Replace(temp, @"(?<=\w)([0-9]+)", "-$1-");
        temp = Regex.Replace(temp, @"[-_\s]+", "-");
        return temp;
    }
    
    public static string ToKebabCase(string value)
    {
        return ToKebabCaseRaw(value).ToLower();
    }
    
    public static string ToKebabCaseUpper(string value)
    {
        return ToKebabCaseRaw(value).ToUpper();
    }
}