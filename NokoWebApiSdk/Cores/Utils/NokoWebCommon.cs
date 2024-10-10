namespace NokoWebApiSdk.Cores;

public static class NokoWebCommon
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

    public static string GetDateTimeIso8601(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

    public static Guid GenerateUuidV7()
    {
        return Guid.CreateVersion7();
    }
    
    public static bool UpdateAnyItemList<T>(IList<T> items, T? item, T newItem, bool multiples = false)
    {
        var checkItem = item ?? newItem;
        if (items.Contains(checkItem))
        {
            // Was Updated
            if (item is null) return false;
            
            // Update Process
            var found = false;
            for (var i = 0; i < items.Count; i++)
            {
                if (!multiples && found) return true;
                if (!Equals(items[i], item)) continue;
                items[i] = newItem;
                found = true;
            }
        }
        else
        {
            // Insert New Item
            items.Add(checkItem);
            return true;
        }

        return false;
    }

    public static bool InsertAnyItemList<T>(IList<T> items, T item)
    {
        // Insert New Item Using Update With Default Value 
        return UpdateAnyItemList(items, default, item);
    }

    public static IDictionary<K, T> InsertAnyMapValues<K, V, T>(IDictionary<K, V> items, T value) 
        where K : notnull
    {
        var m = new Dictionary<K, T>();
        foreach (var item in items)
        {
            m[item.Key] = value;
        }
        return m;
    }

    public static string EndsCut(string? value, string ends)
    {
        if (value is null) return string.Empty;
        return value.EndsWith(ends) ? value[..^ends.Length] : value;
    }
}
