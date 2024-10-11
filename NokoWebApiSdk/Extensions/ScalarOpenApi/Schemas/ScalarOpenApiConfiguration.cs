using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

/// <summary>
/// Internal representation of the configuration for the Scalar API reference.
/// Based on <a href="https://github.com/scalar/scalar/blob/main/documentation/configuration.md">Configuration</a>.
/// </summary>
internal sealed class ScalarOpenApiConfiguration
{
    public required string? Proxy { get; init; }

    public required bool? ShowSidebar { get; init; }

    public required bool? HideModels { get; init; }

    public required bool? HideDownloadButton { get; init; }

    public required bool? HideTestRequestButton { get; init; }

    public required bool? DarkMode { get; init; }

    public required string? ForceDarkModeState { get; init; }

    public required bool? HideDarkModeToggle { get; init; }

    public required string? CustomCss { get; init; }

    public required string? SearchHotKey { get; init; }

    public required IEnumerable<ScalarOpenApiServer>? Servers { get; init; }
    
    public required IDictionary<string, string>? Metadata { get; init; }

    public required DefaultHttpClient? DefaultHttpClient { get; init; }

    public required IDictionary<string, IEnumerable<string>>? HiddenClients { get; init; }

    public required ScalarOpenApiAuthenticationOptions? Authentication { get; init; }

    public required bool? DefaultFonts { get; init; }

    public required bool? DefaultOpenAllTags { get; init; }

    public required string? TagSorter { get; init; }

    public required string? Theme { get; init; }

    public required string? Favicon { get; init; }
}
