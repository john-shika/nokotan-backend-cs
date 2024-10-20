using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Options;

/// <summary>
/// Represents all available options for the Scalar API reference.
/// Based on <a href="https://github.com/scalar/scalar/blob/main/documentation/configuration.md">Configuration</a>.
/// </summary>
public sealed class ScalarOpenApiOptions
{
    public void CopyFrom(ScalarOpenApiOptions other)
    {
        Title = other.Title;
        Favicon = other.Favicon;
        EndpointPathPrefix = other.EndpointPathPrefix;
        OpenApiRoutePattern = other.OpenApiRoutePattern;
        ProxyUrl = other.ProxyUrl;
        ShowSidebar = other.ShowSidebar;
        HideModels = other.HideModels;
        HideDownloadButton = other.HideDownloadButton;
        HideTestRequestButton = other.HideTestRequestButton;
        DarkMode = other.DarkMode;
        ForceThemeMode = other.ForceThemeMode;
        HideDarkModeToggle = other.HideDarkModeToggle;
        CustomCss = other.CustomCss;
        SearchHotKey = other.SearchHotKey;
        Theme = other.Theme;
        WithDefaultFonts = other.WithDefaultFonts;
        DefaultOpenAllTags = other.DefaultOpenAllTags;
        TagSorter = other.TagSorter;
        HiddenClients = other.HiddenClients;
        EnabledClients = other.EnabledClients;
        EnabledTargets = other.EnabledTargets;
        Metadata = other.Metadata;
        Authentication = other.Authentication;
        DefaultHttpClient = other.DefaultHttpClient;
        CdnUrl = other.CdnUrl;
        Servers = other.Servers;
    }
    
    /// <summary>
    /// Metadata title.
    /// </summary>
    /// <value>The default value is <c>'Scalar API Reference -- {documentName}'</c>.</value>
    /// <remarks>You can use <c>{documentName}</c>, and it will be replaced by the actual document name.</remarks>
    public string Title { get; set; } = "Scalar API Reference -- {documentName}";

    /// <summary>
    /// Specify a path or URL to a favicon to be used for the documentation.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public string Favicon { get; set; } = "/favicon.ico";

    /// <summary>
    /// Path prefix to access the documentation.
    /// </summary>
    /// <value>The default value is <c>'/scalar/{documentName}'</c>.</value>
    /// <remarks>You can use <c>{documentName}</c>, and it will be replaced by the actual document name.</remarks>
    public string EndpointPathPrefix { get; set; } = "/scalar/{documentName}";

    /// <summary>
    /// Gets or sets the route pattern of the OpenAPI document.
    /// </summary>
    public string OpenApiRoutePattern { get; set; } = "/openapi/{documentName}.json";

    /// <summary>
    /// Proxy URL for the API requests.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public string? ProxyUrl { get; set; }

    /// <summary>
    /// Whether the sidebar should be shown.
    /// </summary>
    /// <value>The default value is <c>true</c>.</value>
    public bool ShowSidebar { get; set; } = true;

    /// <summary>
    /// Whether models (components.schemas or definitions) should be shown in the sidebar, search, and content.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    public bool HideModels { get; set; }

    /// <summary>
    /// Whether to hide the "Download OpenAPI Specification" button.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    public bool HideDownloadButton { get; set; }

    /// <summary>
    /// Whether to hide the "Test Request" button.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    public bool HideTestRequestButton { get; set; }

    /// <summary>
    /// Whether dark mode is on or off initially.
    /// </summary>
    /// <value>The default value is <c>true</c>.</value>
    public bool DarkMode { get; set; } = true;

    /// <summary>
    /// ForceDarkModeState makes it always this state no matter what <c>'dark' | 'light'</c>.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public ThemeMode? ForceThemeMode { get; set; }

    /// <summary>
    /// Whether to hide the dark mode toggle.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    public bool HideDarkModeToggle { get; set; }

    /// <summary>
    /// Pass custom CSS directly to the component.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public string? CustomCss { get; set; }

    /// <summary>
    /// Key used with CTRL/CMD to open the search modal (e.g. CMD+k).
    /// </summary>
    /// <value>The default value is <c>k</c>.</value>
    public string? SearchHotKey { get; set; }

    /// <summary>
    /// Set color theme.
    /// </summary>
    /// <value>The default value is <see cref="ScalarOpenApiTheme.Purple" />.</value>
    /// <remarks>Select your preferred <see cref="ScalarOpenApiTheme.Purple">ScalarTheme</see>.</remarks>
    public ScalarOpenApiTheme Theme { get; set; } = ScalarOpenApiTheme.Purple;

    /// <summary>
    /// By default, we are using Inter and JetBrains Mono, served by Google Fonts.
    /// </summary>
    /// <value>The default value is <c>true</c>.</value>
    /// <remarks>If you use a different font or just don’t want to use Google Fonts, pass withDefaultFonts: false to the configuration.</remarks>
    public bool WithDefaultFonts { get; set; } = true;

    /// <summary>
    /// By default, only the relevant tag based on the URL is opened.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    /// <remarks>If you want all the tags open by default then set this configuration option.</remarks>
    public bool DefaultOpenAllTags { get; set; }

    /// <summary>
    /// Represents a sorter for tags in the Scalar API reference.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public TagSorter? TagSorter { get; set; }
    
    /// <summary>
    /// You can pass an array of HTTPSnippet clients to hide from the clients menu.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    public bool HiddenClients { get; set; }

    /// <summary>
    /// You can pass an array of HTTPSnippet clients that you want to display in the clients menu.
    /// </summary>
    /// <value>The default value is <see cref="Array.Empty{T}" />.</value>
    /// <remarks>If an empty array is sent, all options will be displayed.</remarks>
    public ScalarOpenApiClient[] EnabledClients { get; set; } = [];

    /// <summary>
    /// You can pass an array of HTTPSnippet targets that you want to display in the clients menu.
    /// </summary>
    /// <value>The default value is <see cref="Array.Empty{T}" />.</value>
    /// <remarks>If an empty array is sent, all options will be displayed.</remarks>
    public ScalarOpenApiTarget[] EnabledTargets { get; set; } = [];

    /// <summary>
    /// You can pass information to the config object to configure meta information out of the box.
    /// </summary>
    public IDictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// To make authentication easier, you can prefill the credentials.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public ScalarOpenApiAuthenticationOptions? Authentication { get; set; }

    /// <summary>
    /// Gets or sets the default HTTP client.
    /// </summary>
    /// <value>The default values are <see cref="ScalarOpenApiTarget.Shell" /> and <see cref="ScalarOpenApiClient.Curl" />.</value>
    public KeyValuePair<ScalarOpenApiTarget, ScalarOpenApiClient> DefaultHttpClient { get; set; } = new(ScalarOpenApiTarget.Shell, ScalarOpenApiClient.Curl);

    /// <summary>
    /// Gets or sets the CDN URL for the API reference.
    /// </summary>
    /// <value>The default value is <i>https://cdn.jsdelivr.net/npm/@scalar/api-reference</i></value>
    /// <remarks>Use this option to load the API reference from a different CDN or local server.</remarks>
    public string CdnUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";

    /// <summary>
    /// Gets or sets the list of servers for the Scalar API reference.
    /// </summary>
    /// <value>A list of <see cref="ScalarOpenApiServer" /> representing the servers. The default value is <c>null</c>.</value>
    /// <remarks>This list will override the servers defined in the OpenAPI document.</remarks>
    public IList<ScalarOpenApiServer>? Servers { get; set; }
}