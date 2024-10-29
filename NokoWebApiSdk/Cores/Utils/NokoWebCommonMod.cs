using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace NokoWebApiSdk.Cores.Utils;

public static class NokoWebCommonMod
{
    // like python3, import string module
    public const string Digits = "0123456789";
    public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
    public const string Punctuation = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
    public const string WhiteSpace = " \t\n\r\v\f";
    public const string Printable = Digits + AlphaUpper + AlphaLower + Punctuation + WhiteSpace;
    
    /// <summary>
    /// IsNoneOrEmpty method checks if a string is null or has a length of zero.
    /// </summary>
    /// <param name="value">The string to be checked.</param>
    /// <returns>True if the string is null or empty; otherwise, false.</returns>
    public static bool IsNoneOrEmpty(string? value)
    {
        return value is null || value.Length == 0;
    }

    /// <summary>
    /// Determines if a string is null, empty, or contains only white-space characters.
    /// This includes several specific white-space and control characters.
    /// </summary>
    /// <param name="value">The string to be checked.</param>
    /// <returns>True if the string is null, empty, or contains only white-space characters; otherwise, false.</returns>
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
               || temp.All(char.IsWhiteSpace); // csharp builtin supported
    }

    /// <summary>
    /// GetDateTimeUtcNow method returns the current date and time in UTC.
    /// </summary>
    /// <returns>The current date and time as a DateTime object in UTC.</returns>
    public static DateTime GetDateTimeUtcNow()
    {
        return DateTime.UtcNow;
    }
    
    /// <summary>
    /// GetDateTimeUtcNowTimestamp method converts a DateTime object to UTC
    /// and returns the number of ticks, rounded down to the nearest millisecond.
    /// </summary>
    /// <returns>The number of ticks, rounded down to the nearest millisecond, for the given DateTime in UTC.</returns>
    public static long GetDateTimeUtcNowTimestamp()
    {
        var dateTime = GetDateTimeUtcNow();
        return GetDateTimeUtcNowTimestamp(dateTime);
    }
    
    /// <summary>
    /// GetDateTimeUtcNowTimestamp method converts a DateTime object to UTC
    /// and returns the number of ticks, rounded down to the nearest millisecond.
    /// </summary>
    /// <param name="dateTime">The DateTime object to be converted and processed.</param>
    /// <returns>The number of ticks, rounded down to the nearest millisecond, for the given DateTime in UTC.</returns>
    public static long GetDateTimeUtcNowTimestamp(DateTime dateTime)
    {
        dateTime = dateTime.ToUniversalTime();
        return dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerMillisecond);
    }

    /// <summary>
    /// GetDateTimeIso8601 method converts a DateTime object to an ISO 8601 formatted string.
    /// </summary>
    /// <param name="dateTime">The DateTime object to be converted.</param>
    /// <returns>A string representing the DateTime in ISO 8601 format.</returns>
    public static string GetDateTimeIso8601(DateTime dateTime)
    {
        dateTime = dateTime.ToUniversalTime();
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

    /// <summary>
    /// GenerateUuidV7 method generates a version 7 UUID (UUIDv7).
    /// </summary>
    /// <returns>A new UUIDv7 as a Guid object.</returns>
    public static Guid GenerateUuidV7()
    {
        return Guid.CreateVersion7();
    }
    
    /// <summary>
    /// UpdateAnyItemList method updates an item in a list or inserts it if not present.
    /// If the `multiples` flag is set to true, it updates all occurrences of the item.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <param name="items">The list of items to update or add to.</param>
    /// <param name="item">The item to be updated. If null, `newItem` is used for both search and insertion.</param>
    /// <param name="newItem">The new item to replace the existing item or to be added if the item is not present.</param>
    /// <param name="multiples">Flag indicating whether to update all occurrences of the item (true) or just the first one (false).</param>
    /// <returns>True if the item was added; otherwise, false.</returns>
    public static bool UpdateAnyItemList<T>(IList<T> items, T? item, T newItem, bool multiples = false)
    {
        var checkItem = item ?? newItem;
        if (items.Contains(checkItem))
        {
            // Was Updated
            if (item is null) return false;

            // Replace Current Item
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

    /// <summary>
    /// InsertAnyItemList method inserts a new item into a list of items
    /// by using the UpdateAnyItemList method with a default value.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <param name="items">The list of items to which the new item will be added.</param>
    /// <param name="item">The item to be inserted into the list.</param>
    /// <returns>True if the item was successfully inserted; otherwise, false.</returns>
    public static bool InsertAnyItemList<T>(IList<T> items, T item)
    {
        items.Add(item);
        return true;
    }

    /// <summary>
    /// InsertAnyMapValues method takes a dictionary of items and a value,
    /// and returns a new dictionary with the same keys but with all values set to the provided value.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionaries.</typeparam>
    /// <typeparam name="TIn">The type of the values in the input dictionary.</typeparam>
    /// <typeparam name="TOut">The type of the values in the output dictionary.</typeparam>
    /// <param name="items">The input dictionary with keys and values.</param>
    /// <param name="value">The value to set for all keys in the new dictionary.</param>
    /// <returns>A new dictionary with the same keys but with all values set to the provided value.</returns>
    public static IDictionary<TKey, TOut> MergeAnyMapValues<TKey, TIn, TOut>(IDictionary<TKey, TIn> items, TOut value)
        where TKey : notnull
    {
        var m = new Dictionary<TKey, TOut>();
        foreach (var item in items)
        {
            m[item.Key] = value;
        }
        return m;
    }

    /// <summary>
    /// EndsCut method takes a string and an ending substring,
    /// and returns the original string without the ending if it matches.
    /// If the original string is null, it returns an empty string.
    /// </summary>
    /// <param name="value">The original string to be processed.</param>
    /// <param name="ends">The ending substring to check for and remove.</param>
    /// <returns>The original string without the ending if it matches; otherwise, the original string.</returns>
    public static string EndsCutString(string? value, string ends)
    {
        if (value is null) return string.Empty;
        return value.EndsWith(ends) ? value[..^ends.Length] : value;
    }

    /// <summary>
    /// GenerateRandomString method generates a random string of the specified size.
    /// It uses the provided source characters or defaults to a printable character set.
    /// </summary>
    /// <param name="size">The desired length of the generated string.</param>
    /// <param name="sources">A string containing possible characters to include in the generated string.</param>
    /// <returns>A randomly generated string of the specified length.</returns>
    public static string GenerateRandomString(int size, string sources = Printable)
    {
        var random = new Random();
        var temp = new StringBuilder(size);
        for (var i = 0; i < size; i++)
        {
            temp.Append(sources[random.Next(sources.Length)]);
        }
        return temp.ToString();
    }


    /// <summary>
    /// EncodeSha256 method takes a string input and encodes it using SHA-256 hash algorithm,
    /// returning the resulting byte array.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <param name="encoding">The encoding to be used. If null, defaults to UTF-8.</param>
    /// <returns>A byte array containing the SHA-256 hash of the input string.</returns>
    public static byte[] EncodeSha256(string value, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(encoding.GetBytes(value));
    }

    
    /// <summary>
    /// EncodeSha3_256 method takes a string input and encodes it using SHA3-256 hash algorithm,
    /// returning the resulting byte array.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <param name="encoding">The encoding to be used. If null, defaults to UTF-8.</param>
    /// <returns>A byte array containing the SHA3-256 hash of the input string.</returns>
    public static byte[] EncodeSha3_256(string value, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var sha3 = SHA3_256.Create();
        return sha3.ComputeHash(encoding.GetBytes(value));
    }
    
    /// <summary>
    /// EncodeSha3_384 method takes a string input and encodes it using SHA3-384 hash algorithm,
    /// returning the resulting byte array.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <param name="encoding">The encoding to be used. If null, defaults to UTF-8.</param>
    /// <returns>A byte array containing the SHA3-384 hash of the input string.</returns>
    public static byte[] EncodeSha3_384(string value, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var sha3 = SHA3_384.Create();
        return sha3.ComputeHash(encoding.GetBytes(value));
    }
    
    /// <summary>
    /// EncodeSha3_512 method takes a string input and encodes it using SHA3-512 hash algorithm,
    /// returning the resulting byte array.
    /// </summary>
    /// /// <param name="value">The string to be encoded.</param>
    /// <param name="encoding">The encoding to be used. If null, defaults to UTF-8.</param>
    /// <returns>A byte array containing the SHA3-512 hash of the input string.</returns>
    public static byte[] EncodeSha3_512(string value, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var sha3 = SHA3_512.Create();
        return sha3.ComputeHash(encoding.GetBytes(value));
    }
    
    /// <summary>
    /// EncodeSha512 method takes a string input and encodes it using SHA-512 hash algorithm,
    /// returning the resulting byte array.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <param name="encoding">The encoding to be used. If null, defaults to UTF-8.</param>
    /// <returns>A byte array containing the SHA-512 hash of the input string.</returns>
    public static byte[] EncodeSha512(string value, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        using var sha512 = SHA512.Create();
        return sha512.ComputeHash(encoding.GetBytes(value));
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Generic version to check for a specific attribute type.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute to check for.</typeparam>
    /// <param name="type">The type to inspect for the attribute.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute<TAttribute>(Type type)
        where TAttribute : Attribute
    {
        return type.GetCustomAttribute<TAttribute>() is not null;
    }

    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="type">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to check for.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute(Type type, Type attributeType)
    {
        return type.GetCustomAttribute(attributeType) is not null;
    }


    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Generic version to get a specific attribute type.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute to retrieve.</typeparam>
    /// <param name="type">The type to inspect for the attribute.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static TAttribute? GetAttribute<TAttribute>(Type type)
        where TAttribute : Attribute
    {
        return type.GetCustomAttribute<TAttribute>();
    }


    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="type">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to retrieve.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static Attribute? GetAttribute(Type type, Type attributeType)
    {
        return type.GetCustomAttribute(attributeType);
    }

    /// <summary>
    /// StripGenericMarker method takes a Type object,
    /// checks if the type name contains a backtick (`).
    /// If it does, it trims the name up to the backtick,
    /// effectively removing the generic type indicator.
    /// If not, it returns the original type name.
    /// </summary>
    /// <param name="type">The Type object to be processed.</param>
    /// <returns>A string containing the type name without the generic type indicator, if present.</returns>
    public static string StripGenericMarker(Type? type)
    {
        if (type is null) throw new Exception("Type Is None");
    
        var index = type.Name.IndexOf('`');
        return index < 0 ? type.Name : type.Name[..index];
    }

    /// <summary>
    /// GetRootNamespaceSegment method takes a namespace string,
    /// splits it by dots and returns the first segment (root namespace).
    /// If the namespace string is null, empty, or whitespace, it throws an exception.
    /// </summary>
    /// <param name="value">The namespace string to be processed.</param>
    /// <returns>The first segment of the namespace string (root namespace).</returns>
    public static string GetRootNamespaceSegment(string? value)
    {
        if (IsNoneOrEmptyWhiteSpace(value)) throw new Exception("Value is None or Empty WhiteSpace");
    
        var parts = value!.Split('.');
        return parts.Length > 0 ? parts[0] : value;
    }
    
    /// <summary>
    /// TrimLastNamespaceSegment method takes a namespace string,
    /// splits it by dots and returns a new string with the last segment removed.
    /// If there is only one segment, it returns the original string.
    /// </summary>
    /// <param name="value">The namespace string to be processed.</param>
    /// <returns>A new string with the last segment removed, or the original string if there is only one segment.</returns>
    public static string TrimLastNamespaceSegment(string? value)
    {
        if (IsNoneOrEmptyWhiteSpace(value)) throw new Exception("Value Is None Or Empty WhiteSpace");
    
        var parts = value!.Split('.');
        return parts.Length > 1 ? string.Join('.', parts, 0, parts.Length - 1) : value;
    }

    /// <summary>
    /// PrintAssemblyName debug test to get Root Namespace
    /// </summary>
    public static void PrintAssemblyName()
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;
        var @namespace = entryAssembly.GetName().Name;
        Console.WriteLine($"Namespace (AssemblyName): {@namespace}");
    }
}
