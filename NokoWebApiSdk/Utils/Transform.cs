using System.Globalization;
using System.Text.RegularExpressions;

namespace NokoWebApiSdk.Utils;

public static class Transform
{
    public static string ToTitleCase(string value)
    {
        if (Common.IsNoneOrEmptyOrWhiteSpace(value)) return string.Empty;
        var temp = Regex.Replace(value.Trim(), @"(?<!^)(?=[A-Z])|[-_\s]+", " ");
        temp = Regex.Replace(temp, @"[-_\s]+", " ");
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(temp);
    }
    
    public static string ToPascalCase(string value)
    {
        var temp = ToTitleCase(value);
        return temp.Replace(" ", "");
    }
    
    public static string ToCamelCase(string value)
    {
        var temp = ToPascalCase(value);
        return temp.Length switch
        {
            0 => string.Empty,
            1 => string.Empty + char.ToLower(temp[0]),
            _ => char.ToLower(temp[0]) + temp[1..]
        };
    }
    
    private static string ToSnakeCaseRaw(string value)
    {
        if (Common.IsNoneOrEmptyOrWhiteSpace(value)) return string.Empty;
        var temp = Regex.Replace(value.Trim(), @"(?<!^)(?=[A-Z])|[-_\s]+", "_");
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
        if (Common.IsNoneOrEmptyOrWhiteSpace(value)) return string.Empty;
        var temp = Regex.Replace(value.Trim(), @"(?<!^)(?=[A-Z])|[-_\s]+", "-");
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