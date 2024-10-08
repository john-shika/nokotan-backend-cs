namespace NokoWebApiSdk.Annotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UniqueKeyAttribute(bool isUnique) : Attribute
{
    public bool IsUnique { get; } = isUnique;

    public UniqueKeyAttribute() : this(true)
    {
        // Nothing Todo...
    }
}