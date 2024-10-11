using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace NokoWebApiSdk.Extensions.OpenApi.Scalar.Enums;

/// <summary>
/// Specifies the sorting options for tags in the Scalar API reference.
/// </summary>
[EnumExtensions]
public enum TagSorter
{
    /// <summary>
    /// Sort tags alphabetically.
    /// </summary>
    [Description("alpha")]
    Alpha
}