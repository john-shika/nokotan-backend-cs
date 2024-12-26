using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NokoWebApiSdk.Cores.Utils;

public static class NokoCommonMod
{
    // like python3, import string module
    public const string Digits = "0123456789";
    public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
    public const string Punctuation = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
    public const string WhiteSpace = " \t\n\r\v\f";
    public const string Printable = Digits + AlphaUpper + AlphaLower + Punctuation + WhiteSpace;
    
    public delegate void ForEachEntryHandler<in TKey, in TValue>(TKey key, TValue value);

    /// <summary>
    /// Iterates over each element in the enumerable and invokes the handler with the index and value of each element.
    /// </summary>
    /// <typeparam name="TValue">The type of the elements in the enumerable.</typeparam>
    /// <param name="enumerable">The collection of elements to iterate over.</param>
    /// <param name="handler">The delegate to handle each element and its index.</param>
    public static void ForEach<TValue>(IEnumerable<TValue> enumerable, ForEachEntryHandler<int, TValue> handler)
    {
        var i = 0;
        foreach (var value in enumerable)
        {
            handler.Invoke(i, value);
            i++;
        }
    }

    /// <summary>
    /// Iterates over each key-value pair in the dictionary and invokes the handler with the key and value of each entry.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to iterate over.</param>
    /// <param name="handler">The delegate to handle each key-value pair.</param>
    public static void ForEach<TKey, TValue>(IDictionary<TKey, TValue> dictionary, ForEachEntryHandler<TKey, TValue> handler)
    {
        foreach (var kv in dictionary)
        {
            handler.Invoke(kv.Key, kv.Value);
        }
    }

    /// <summary>
    /// Splits the string by the specified separator, trims each substring, and invokes the handler with the index and substring.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="separator">The separator to use for splitting the string.</param>
    /// <param name="handler">The delegate to handle each substring and its index.</param>
    public static void ForEachStringSplit(string value, string separator, ForEachEntryHandler<int, string> handler)
    {
        var i = 0;
        foreach (var word in value.Split(separator).Select((x) => x.Trim()))
        {
            if (word.Length == 0) continue;
            handler.Invoke(i, word);
            i++;
        }
    }

    /// <summary>
    /// Splits the string using the specified regular expression pattern, trims each substring, and invokes the handler with the index and substring.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="pattern">The regular expression pattern to use for splitting the string.</param>
    /// <param name="handler">The delegate to handle each substring and its index.</param>
    public static void ForEachStringSplit(string value, Regex pattern, ForEachEntryHandler<int, string> handler)
    {
        var i = 0;
        foreach (var word in pattern.Split(value).Select((x) => x.Trim()))
        {
            if (word.Length == 0) continue;
            handler.Invoke(i, word);
            i++;
        }
    }
    
    /// <summary>
    /// Displays a fatal error message in red and formats the message if necessary.
    /// </summary>
    /// <param name="message">The main error message to display.</param>
    /// <param name="args">Optional parameters for additional formatted messages.</param>
    public static void FatalError(string message, params object?[] args)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (args is { Length: > 0 })
        {
            message = string.Format(message, args);
        }

        Console.WriteLine(message);
        Console.ResetColor();
        Environment.Exit(1);
    }
    
    /// <summary>
    /// IsNoneOrEmpty method checks if a string is null or has a length of zero.
    /// </summary>
    /// <param name="value">The string to be checked.</param>
    /// <returns>True if the string is null or empty; otherwise, false.</returns>
    public static bool IsNoneOrEmpty(string? value)
    {
        return value is null or "";
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
            // Insert checkItem
            return InsertAnyItemList(items, checkItem);
        }
        return false;
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
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to check for.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute(Assembly element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType) is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to check for.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute(Module element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType) is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to check for.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute(MemberInfo element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType) is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to check for.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute(ParameterInfo element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType) is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute<T>(Assembly element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>() is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute<T>(Module element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>() is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute<T>(MemberInfo element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>() is not null;
    }
    
    /// <summary>
    /// Determines if the specified type has the given attribute.
    /// Non-generic version using Type to check for a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>True if the specified attribute is present; otherwise, false.</returns>
    public static bool HasAttribute<T>(ParameterInfo element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>() is not null;
    }

    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to retrieve.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static Attribute? GetAttribute(Assembly element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType);
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to retrieve.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static Attribute? GetAttribute(Module element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType);
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to retrieve.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static Attribute? GetAttribute(MemberInfo element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType);
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <param name="attributeType">The type of the attribute to retrieve.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static Attribute? GetAttribute(ParameterInfo element, Type attributeType)
    {
        return element.GetCustomAttribute(attributeType);
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static T? GetAttribute<T>(Assembly element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>();
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static T? GetAttribute<T>(Module element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>();
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static T? GetAttribute<T>(MemberInfo element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>();
    }
    
    /// <summary>
    /// Gets the specified attribute from the given type.
    /// Non-generic version using Type to get a specific attribute type.
    /// </summary>
    /// <param name="element">The type to inspect for the attribute.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static T? GetAttribute<T>(ParameterInfo element) 
        where T : Attribute
    {
        return element.GetCustomAttribute<T>();
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <param name="attributeType">The type of attribute to retrieve.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<Attribute> GetAttributes(Assembly element, Type attributeType) 
    {
        var attributes = Attribute.GetCustomAttributes(element, attributeType);
        return attributes.Where(attributeType.IsInstanceOfType);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <param name="attributeType">The type of attribute to retrieve.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<Attribute> GetAttributes(Module element, Type attributeType) 
    {
        var attributes = Attribute.GetCustomAttributes(element, attributeType);
        return attributes.Where(attributeType.IsInstanceOfType);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <param name="attributeType">The type of attribute to retrieve.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<Attribute> GetAttributes(MemberInfo element, Type attributeType) 
    {
        var attributes = Attribute.GetCustomAttributes(element, attributeType);
        return attributes.Where(attributeType.IsInstanceOfType);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <param name="attributeType">The type of attribute to retrieve.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<Attribute> GetAttributes(ParameterInfo element, Type attributeType) 
    {
        var attributes = Attribute.GetCustomAttributes(element, attributeType);
        return attributes.Where(attributeType.IsInstanceOfType);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<T> GetAttributes<T>(Assembly element) 
        where T : Attribute
    {
        var attributes = Attribute.GetCustomAttributes(element);
        return attributes.Where((attribute) => attribute is T).Select((attribute) => (attribute as T)!);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<T> GetAttributes<T>(Module element) 
        where T : Attribute
    {
        var attributes = Attribute.GetCustomAttributes(element);
        return attributes.Where((attribute) => attribute is T).Select((attribute) => (attribute as T)!);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<T> GetAttributes<T>(MemberInfo element) 
        where T : Attribute
    {
        var attributes = Attribute.GetCustomAttributes(element);
        return attributes.Where((attribute) => attribute is T).Select((attribute) => (attribute as T)!);
    }
    
    /// <summary>
    /// Retrieves the custom attributes of a specified type from an assembly and returns them as an enumerable collection.
    /// </summary>
    /// <param name="element">The assembly from which to retrieve the custom attributes.</param>
    /// <returns>An enumerable collection of attributes of the specified type from the assembly.</returns>
    public static IEnumerable<T> GetAttributes<T>(ParameterInfo element) 
        where T : Attribute
    {
        var attributes = Attribute.GetCustomAttributes(element);
        return attributes.Where((attribute) => attribute is T).Select((attribute) => (attribute as T)!);
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
    /// Converts a list of objects into a tuple with 2 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 2 elements.</param>
    /// <returns>A tuple containing 2 elements of types T1 and T2.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 2 elements.</exception>
    public static (T1, T2) ToTuple<T1, T2>(IList<object> items)
    {
        if (items.Count != 2)
        {
            throw new ArgumentException("List must contain exactly 2 elements");
        }
        return ((T1)items[0], (T2)items[1]);
    }

    /// <summary>
    /// Converts a list of objects into a tuple with 3 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 3 elements.</param>
    /// <returns>A tuple containing 3 elements of types T1, T2 and T3.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 3 elements.</exception>
    public static (T1, T2, T3) ToTuple<T1, T2, T3>(IList<object> items)
    {
        if (items.Count != 3)
        {
            throw new ArgumentException("List must contain exactly 3 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2]);
    }

    /// <summary>
    /// Converts a list of objects into a tuple with 4 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 4 elements.</param>
    /// <returns>A tuple containing 4 elements of types T1, T2, T3 and T4.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 4 elements.</exception>
    public static (T1, T2, T3, T4) ToTuple<T1, T2, T3, T4>(IList<object> items)
    {
        if (items.Count != 4)
        {
            throw new ArgumentException("List must contain exactly 4 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3]);
    }

    /// <summary>
    /// Converts a list of objects into a tuple with 5 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 5 elements.</param>
    /// <returns>A tuple containing 5 elements of types T1, T2, T3, T4 and T5.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 5 elements.</exception>
    public static (T1, T2, T3, T4, T5) ToTuple<T1, T2, T3, T4, T5>(IList<object> items)
    {
        if (items.Count != 5)
        {
            throw new ArgumentException("List must contain exactly 5 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4]);
    }

    /// <summary>
    /// Converts a list of objects into a tuple with 6 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 6 elements.</param>
    /// <returns>A tuple containing 6 elements of types T1, T2, T3, T4, T5 and T6.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 6 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6) ToTuple<T1, T2, T3, T4, T5, T6>(IList<object> items)
    {
        if (items.Count != 6)
        {
            throw new ArgumentException("List must contain exactly 6 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 7 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 7 elements.</param>
    /// <returns>A tuple containing 7 elements of types T1, T2, T3, T4, T5, T6 and T7.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 7 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7) ToTuple<T1, T2, T3, T4, T5, T6, T7>(IList<object> items)
    {
        if (items.Count != 7)
        {
            throw new ArgumentException("List must contain exactly 7 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 8 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 8 elements.</param>
    /// <returns>A tuple containing 8 elements of types T1, T2, T3, T4, T5, T6, T7 and T8.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 8 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(IList<object> items)
    {
        if (items.Count != 8)
        {
            throw new ArgumentException("List must contain exactly 8 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 9 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <typeparam name="T9">The type of the 9 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 9 elements.</param>
    /// <returns>A tuple containing 9 elements of types T1, T2, T3, T4, T5, T6, T7, T8 and T9.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 9 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(IList<object> items)
    {
        if (items.Count != 9)
        {
            throw new ArgumentException("List must contain exactly 9 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7], (T9)items[8]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 10 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <typeparam name="T9">The type of the 9 element in the tuple.</typeparam>
    /// <typeparam name="T10">The type of the 10 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 10 elements.</param>
    /// <returns>A tuple containing 10 elements of types T1, T2, T3, T4, T5, T6, T7, T8, T9 and T10.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 10 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(IList<object> items)
    {
        if (items.Count != 10)
        {
            throw new ArgumentException("List must contain exactly 10 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7], (T9)items[8], (T10)items[9]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 11 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <typeparam name="T9">The type of the 9 element in the tuple.</typeparam>
    /// <typeparam name="T10">The type of the 10 element in the tuple.</typeparam>
    /// <typeparam name="T11">The type of the 11 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 11 elements.</param>
    /// <returns>A tuple containing 11 elements of types T1, T2, T3, T4, T5, T6, T7, T8, T9, T10 and T11.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 11 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(IList<object> items)
    {
        if (items.Count != 11)
        {
            throw new ArgumentException("List must contain exactly 11 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7], (T9)items[8], (T10)items[9], (T11)items[10]);
    }
    
    /// <summary>
    /// Converts a list of objects into a tuple with 12 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <typeparam name="T9">The type of the 9 element in the tuple.</typeparam>
    /// <typeparam name="T10">The type of the 10 element in the tuple.</typeparam>
    /// <typeparam name="T11">The type of the 11 element in the tuple.</typeparam>
    /// <typeparam name="T12">The type of the 12 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 12 elements.</param>
    /// <returns>A tuple containing 12 elements of types T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11 and T12.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 12 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(IList<object> items)
    {
        if (items.Count != 12)
        {
            throw new ArgumentException("List must contain exactly 12 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7], (T9)items[8], (T10)items[9], (T11)items[10], (T12)items[11]);
    }

    /// <summary>
    /// Converts a list of objects into a tuple with 13 elements.
    /// </summary>
    /// <typeparam name="T1">The type of the 1 element in the tuple.</typeparam>
    /// <typeparam name="T2">The type of the 2 element in the tuple.</typeparam>
    /// <typeparam name="T3">The type of the 3 element in the tuple.</typeparam>
    /// <typeparam name="T4">The type of the 4 element in the tuple.</typeparam>
    /// <typeparam name="T5">The type of the 5 element in the tuple.</typeparam>
    /// <typeparam name="T6">The type of the 6 element in the tuple.</typeparam>
    /// <typeparam name="T7">The type of the 7 element in the tuple.</typeparam>
    /// <typeparam name="T8">The type of the 8 element in the tuple.</typeparam>
    /// <typeparam name="T9">The type of the 9 element in the tuple.</typeparam>
    /// <typeparam name="T10">The type of the 10 element in the tuple.</typeparam>
    /// <typeparam name="T11">The type of the 11 element in the tuple.</typeparam>
    /// <typeparam name="T12">The type of the 12 element in the tuple.</typeparam>
    /// <typeparam name="T13">The type of the 13 element in the tuple.</typeparam>
    /// <param name="items">The list of objects to be converted into a tuple. The list must contain exactly 13 elements.</param>
    /// <returns>A tuple containing 13 elements of types T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12 and T13.</returns>
    /// <exception cref="ArgumentException">Thrown when the list does not contain exactly 13 elements.</exception>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IList<object> items)
    {
        if (items.Count != 13)
        {
            throw new ArgumentException("List must contain exactly 13 elements");
        }
        return ((T1)items[0], (T2)items[1], (T3)items[2], (T4)items[3], (T5)items[4], (T6)items[5], (T7)items[6], (T8)items[7], (T9)items[8], (T10)items[9], (T11)items[10], (T12)items[11], (T13)items[12]);
    }

    /// <summary>
    /// Merges new items into an existing list if they do not already exist in the list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the lists. Must implement IComparable<T> and IEquatable<T>.</typeparam>
    /// <param name="items">The existing list of items.</param>
    /// <param name="newItems">The list of new items to be added if they are not already present in the existing list.</param>
    /// <returns>The merged list with all unique items.</returns>
    public static IList<T> MergeAnyItemsList<T>(IList<T> items, IList<T> newItems)
        where T : IComparable<T>, IEquatable<T>
    {
        foreach (var item in newItems)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }
        return items;
    }
    
    /// <summary>
    /// Encodes a byte array to a Base64 string.
    /// </summary>
    /// <param name="data">The byte array to encode.</param>
    /// <returns>A Base64 encoded string.</returns>
    public static string EncodeBase64(byte[] data)
    {
        return Convert.ToBase64String(data);
    }
    
    /// <summary>
    /// Encodes a byte array to a URL-safe base64 string without padding.
    /// </summary>
    /// <param name="data">The byte array to encode.</param>
    /// <returns>A URL-safe base64 encoded string.</returns>
    public static string EncodeBase64UrlSafe(byte[] data)
    {
        // Standard Base64 encoding
        var base64 = Convert.ToBase64String(data);

        // Replace '+' with '-' and '/' with '_', and remove any trailing '='
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }
    
    /// <summary>
    /// Decodes a Base64 string to a byte array.
    /// </summary>
    /// <param name="base64">The Base64 encoded string to decode.</param>
    /// <returns>The decoded byte array.</returns>
    public static byte[] DecodeBase64(string base64)
    {
        return Convert.FromBase64String(base64);
    }

    /// <summary>
    /// Decodes a URL-safe base64 string to a byte array.
    /// </summary>
    /// <param name="base64">The URL-safe base64 encoded string to decode.</param>
    /// <returns>The decoded byte array.</returns>
    public static byte[] DecodeBase64UrlSafe(string base64)
    {
        // Replace '-' with '+' and '_' with '/'
        var temp = base64.Replace('-', '+').Replace('_', '/');

        // Add padding if necessary
        switch (base64.Length % 4)
        {
            case 2: temp += "=="; break;
            case 3: temp += "="; break;
        }

        // Convert from Base64 string to byte array
        return Convert.FromBase64String(temp);
    }
}
