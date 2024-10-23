using System.Security.Cryptography;
using System.Text;

namespace NokoWebApiSdk.Cores.Utils;

public static class NokoWebCommonMod
{
    // python3: import string
    public const string Digits = "0123456789";
    public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
    public const string Punctuation = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
    public const string WhiteSpace = " \t\n\r\v\f";
    public const string Printable = Digits + AlphaUpper + AlphaLower + Punctuation + WhiteSpace;
    
    public static bool IsNoneOrEmpty(string? value)
    {
        return value is null || value.Length == 0;
    }

    public static bool IsNoneOrEmptyWhiteSpace(string? value)
    {
        if (IsNoneOrEmpty(value)) return true;
        var temp = value!.Trim();
        return temp == "" 
               || temp == "\0" // is NULL
               || temp == "\xC2" // just NB
               || temp == "\xA0" // just SP
               || temp == "\xC2\xA0" // NBSP
               || temp == "\t" // TAB
               || temp == "\r" // CR
               || temp == "\n" // LF
               || temp == "\r\n" // CRLF
               || temp == "\v" // Vertical Tab (VT) ASCII 11
               || temp == "\f" // Form Feed (FF) ASCII 12
               || temp == "\v\f" // maybe combine of them
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

    public static string GenerateRandomString(int size, string sources = Printable)
    {
        var random = new Random();
        var stringBuilder = new StringBuilder(size);

        for (var i = 0; i < size; i++)
        {
            stringBuilder.Append(sources[random.Next(sources.Length)]);
        }

        return stringBuilder.ToString();
    }

    public static byte[] EncodeSha512(string value)
    {
        using (var sha512 = SHA512.Create())
        {
            return sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
