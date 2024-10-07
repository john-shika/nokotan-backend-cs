namespace NokoWebApiExtra.Utils;

public static class Common
{
    public static bool IsNoneOrEmpty(string? value)
    {
        return value is null || value.Length == 0;
    }

    public static bool IsNoneOrWhiteSpace(string? value)
    {
        return value is null || value.All(char.IsWhiteSpace);
    }

    public static bool IsNoneOrEmptyOrWhiteSpace(string value)
    {
        return IsNoneOrEmpty(value) || IsNoneOrWhiteSpace(value);
    }

    public static DateTime TruncateToMilliseconds(DateTime dateTime)
    {
        return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerMillisecond), dateTime.Kind);
    }

    public static DateTime GetDateTimeUtcNowInMilliseconds()
    {
        var dateTimeUtcNow = DateTime.UtcNow;
        return TruncateToMilliseconds(dateTimeUtcNow);
    }

    public static Guid GenerateUuidV7()
    {
        return Guid.CreateVersion7();
    }
}
