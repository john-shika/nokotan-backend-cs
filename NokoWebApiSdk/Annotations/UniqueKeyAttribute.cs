﻿namespace NokoWebApiSdk.Annotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class UniqueKeyAttribute : Attribute
{
    public bool IsUnique { get; }

    public UniqueKeyAttribute() : this(true)
    {
        // do nothing...
    }

    public UniqueKeyAttribute(bool isUnique)
    {
        IsUnique = isUnique;
    }
}