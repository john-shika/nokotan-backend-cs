using System.Globalization;
using System.Text.RegularExpressions;

namespace NokoWebApiSdk.Utils;

public static class Transform
{
    public static string ToCamelCase(string val)
    {
        if (Common.IsNoneOrEmptyOrWhiteSpace(val)) return string.Empty;
        val = Regex.Replace(val.Trim(), @"[-_\s]+", " ");
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        val = textInfo.ToTitleCase(val).Replace(" ", "");
        return char.ToLowerInvariant(val[0]) + val[1..];
    }
}