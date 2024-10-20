namespace NokoWebApiSdk.Cores.Utils;

public static class NokoWebCommon
{
    public static bool IsNoneOrEmpty(string? value)
    {
        return value is null || value.Length == 0;
    }

    public static bool IsNoneOrEmptyWhiteSpace(string? value)
    {
        if (IsNoneOrEmpty(value)) return true;
        var temp = value!.Trim();
        return temp == "" 
               || temp == "\0"
               || temp == "\xA0"
               || temp == "\t"
               || temp == "\r"
               || temp == "\n"
               || temp == "\r\n"
               || temp.All(char.IsWhiteSpace);
    }

    public static DateTime GetDateTimeUtcNow()
    {
        return DateTime.UtcNow;
    }
    
    public static long GetDateTimeUtcNowTimestamp()
    {
        var dateTime = GetDateTimeUtcNow();
        return GetDateTimeUtcNowTimestamp(dateTime);
    }
    
    public static long GetDateTimeUtcNowTimestamp(DateTime dateTime)
    {
        dateTime = dateTime.ToUniversalTime();
        return dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerMillisecond);
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
