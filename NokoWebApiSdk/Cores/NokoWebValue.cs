namespace NokoWebApiSdk.Cores;

public class NokoWebValue<T>(T value) : IComparable<NokoWebValue<T>>, IEquatable<NokoWebValue<T>>
    where T : IComparable<T>, IEquatable<T>
{
    private readonly T[] _memoize = [value];

    public T Value
    {
        get => _memoize[0];
        set => _memoize[0] = value;
    }

    public int CompareTo(T other)
    {
        return Value.CompareTo(other);
    }
    
    public int CompareTo(NokoWebValue<T>? other)
    {
        return other is not null ? Value.CompareTo(other.Value) : 1;
    }

    public bool Equals(T other)
    {
        return Value.Equals(other);
    }
    
    public bool Equals(NokoWebValue<T>? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            NokoWebValue<T> other => Equals(other),
            T other => Equals(other),
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string? ToString()
    {
        return Value.ToString();
    }
    
    
    public static bool operator <(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) < 0;
    }
    
    public static bool operator <(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is not null && left.CompareTo(right.Value) < 0;
    }
    
    public static bool operator <(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => false,
            NokoWebValue<T> other => left < other,
            T other => left < other,
            _ => false
        };
    }

    public static bool operator >(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) > 0;
    }
    
    public static bool operator >(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is null || left.CompareTo(right.Value) > 0;
    }
    
    public static bool operator >(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => true,
            NokoWebValue<T> other => left > other,
            T other => left > other,
            _ => false
        };
    }
    
    public static bool operator ==(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) == 0;
    }
    
    public static bool operator ==(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is not null && left.CompareTo(right) == 0;
    }
    
    public static bool operator ==(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => false,
            NokoWebValue<T> other => left == other,
            T other => left == other,
            _ => false
        };
    }
    
    public static bool operator !=(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) != 0;
    }
    
    public static bool operator !=(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is null || left.CompareTo(right) != 0;
    }
    
    public static bool operator !=(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => true,
            NokoWebValue<T> other => left != other,
            T other => left != other,
            _ => true
        };
    }

    public static bool operator <=(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) <= 0;
    }
    
    public static bool operator <=(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is not null && left.CompareTo(right) <= 0;
    }
    
    public static bool operator <=(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => false,
            NokoWebValue<T> other => left <= other,
            T other => left <= other,
            _ => false
        };
    }

    public static bool operator >=(NokoWebValue<T> left, T right)
    {
        return left.CompareTo(right) >= 0;
    }
    
    public static bool operator >=(NokoWebValue<T> left, NokoWebValue<T>? right)
    {
        return right is null || left.CompareTo(right) >= 0;
    }
    
    public static bool operator >=(NokoWebValue<T> left, object? right)
    {
        return right switch
        {
            null => true,
            NokoWebValue<T> other => left >= other,
            T other => left >= other,
            _ => false
        };
    }
}