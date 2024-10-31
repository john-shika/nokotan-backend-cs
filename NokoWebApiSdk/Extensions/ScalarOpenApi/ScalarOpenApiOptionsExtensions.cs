using System.Diagnostics.CodeAnalysis;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi;

/// <summary>
/// Provides extension methods for configuring <see cref="ScalarOpenApiOptions" />.
/// </summary>
public static class ScalarOpenApiOptionsExtensions
{
    /// <summary>
    /// Sets the title of the page.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="title">The title to set.</param>
    public static ScalarOpenApiOptions WithTitle(this ScalarOpenApiOptions options, string title)
    {
        options.Title = title;
        return options;
    }

    /// <summary>
    /// Sets the favicon path or URL that will be used for the documentation.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="favicon">The path or URL to the favicon.</param>
    public static ScalarOpenApiOptions WithFavicon(this ScalarOpenApiOptions options, string favicon)
    {
        options.Favicon = favicon;
        return options;
    }

    /// <summary>
    /// Sets the path prefix to access the documentation.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="prefix">The path prefix to set.</param>
    public static ScalarOpenApiOptions WithEndpointPrefix(this ScalarOpenApiOptions options, string prefix)
    {
        options.EndpointPathPrefix = prefix;
        return options;
    }

    /// <summary>
    /// Sets the proxy URL for the API requests.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="proxyUrl">The proxy URL to set.</param>
    public static ScalarOpenApiOptions WithProxyUrl(this ScalarOpenApiOptions options, string proxyUrl)
    {
        options.ProxyUrl = proxyUrl;
        return options;
    }

    /// <summary>
    /// Sets whether the sidebar should be shown.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="showSidebar">Whether to show the sidebar.</param>
    public static ScalarOpenApiOptions WithSidebar(this ScalarOpenApiOptions options, bool showSidebar)
    {
        options.ShowSidebar = showSidebar;
        return options;
    }

    /// <summary>
    /// Sets whether models should be shown in the sidebar, search, and content.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="showModels">Whether to show models.</param>
    public static ScalarOpenApiOptions WithModels(this ScalarOpenApiOptions options, bool showModels)
    {
        options.HideModels = !showModels;
        return options;
    }

    /// <summary>
    /// Sets whether to show the "Download OpenAPI Specification" button.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="showDownloadButton">Whether to show the download button.</param>
    public static ScalarOpenApiOptions WithDownloadButton(this ScalarOpenApiOptions options, bool showDownloadButton)
    {
        options.HideDownloadButton = !showDownloadButton;
        return options;
    }

    /// <summary>
    /// Sets whether to show the "Test Request" button.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="showTestRequestButton">Whether to show the test request button.</param>
    public static ScalarOpenApiOptions WithTestRequestButton(this ScalarOpenApiOptions options, bool showTestRequestButton)
    {
        options.HideTestRequestButton = !showTestRequestButton;
        return options;
    }

    /// <summary>
    /// Sets whether dark mode is on or off initially.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="darkMode">Whether dark mode is on or off initially.</param>
    public static ScalarOpenApiOptions WithDarkMode(this ScalarOpenApiOptions options, bool darkMode)
    {
        options.DarkMode = darkMode;
        return options;
    }

    /// <summary>
    /// Forces the theme mode to always be the specified state.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="forceScalarOpenApiThemeModes">The theme mode to force.</param>
    /// <returns></returns>
    public static ScalarOpenApiOptions WithForceThemeMode(this ScalarOpenApiOptions options, ScalarOpenApiThemeModes forceScalarOpenApiThemeModes)
    {
        options.ForceThemeMode = forceScalarOpenApiThemeModes;
        return options;
    }

    /// <summary>
    /// Sets whether to show the dark mode toggle.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="showDarkModeToggle">Whether to show the dark mode toggle.</param>
    public static ScalarOpenApiOptions WithDarkModeToggle(this ScalarOpenApiOptions options, bool showDarkModeToggle)
    {
        options.HideDarkModeToggle = !showDarkModeToggle;
        return options;
    }

    /// <summary>
    /// Sets custom CSS directly to the component.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="customCss">The custom CSS to set.</param>
    public static ScalarOpenApiOptions WithCustomCss(this ScalarOpenApiOptions options, string customCss)
    {
        options.CustomCss = customCss;
        return options;
    }

    /// <summary>
    /// Sets the key used with CTRL/CMD to open the search modal.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="searchHotKey">The search hotkey to set.</param>
    public static ScalarOpenApiOptions WithSearchHotKey(this ScalarOpenApiOptions options, string searchHotKey)
    {
        options.SearchHotKey = searchHotKey;
        return options;
    }

    /// <summary>
    /// Sets the color theme.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="themes">The theme to set.</param>
    public static ScalarOpenApiOptions WithTheme(this ScalarOpenApiOptions options, ScalarOpenApiThemes themes)
    {
        options.Themes = themes;
        return options;
    }

    /// <summary>
    /// Sets whether to use the default fonts.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="useDefaultFonts">Whether to use the default fonts.</param>
    public static ScalarOpenApiOptions WithDefaultFonts(this ScalarOpenApiOptions options, bool useDefaultFonts)
    {
        options.WithDefaultFonts = useDefaultFonts;
        return options;
    }

    /// <summary>
    /// Sets whether to open all tags by default.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="useOpenAllTags">Whether to open all tags by default.</param>
    public static ScalarOpenApiOptions WithDefaultOpenAllTags(this ScalarOpenApiOptions options, bool useOpenAllTags)
    {
        options.DefaultOpenAllTags = useOpenAllTags;
        return options;
    }

    /// <summary>
    /// Adds a server to the list of servers in the <see cref="ScalarOpenApiOptions" />.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="server">The <see cref="ScalarOpenApiServer" /> to add.</param>
    public static ScalarOpenApiOptions AddServer(this ScalarOpenApiOptions options, ScalarOpenApiServer server)
    {
        options.Servers ??= new List<ScalarOpenApiServer>();
        options.Servers.Add(server);
        return options;
    }

    /// <summary>
    /// Adds a server to the list of servers in the <see cref="ScalarOpenApiOptions" /> using a URL.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="url">The URL of the server to add.</param>
    public static ScalarOpenApiOptions AddServer(this ScalarOpenApiOptions options, string url)
    {
        return options.AddServer(new ScalarOpenApiServer(url));
    }

    /// <summary>
    /// Adds metadata to the configuration.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    public static ScalarOpenApiOptions AddMetadata(this ScalarOpenApiOptions options, string key, string value)
    {
        options.Metadata ??= new Dictionary<string, string>();
        options.Metadata.Add(key, value);
        return options;
    }

    /// <summary>
    /// Sets the tag sorter for the <see cref="ScalarOpenApiOptions" />.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="scalarOpenApiTagSorters">The <see cref="ScalarOpenApiTagSorters" /> to use.</param>
    public static ScalarOpenApiOptions WithTagSorter(this ScalarOpenApiOptions options, ScalarOpenApiTagSorters scalarOpenApiTagSorters)
    {
        options.TagSorter = scalarOpenApiTagSorters;
        return options;
    }

    /// <summary>
    /// Sets the preferred authentication scheme.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="preferredScheme">The preferred authentication scheme.</param>
    public static ScalarOpenApiOptions WithPreferredScheme(this ScalarOpenApiOptions options, string preferredScheme)
    {
        options.Authentication ??= new ScalarOpenApiAuthenticationOptions();
        options.Authentication.PreferredSecurityScheme = preferredScheme;
        return options;
    }

    /// <summary>
    /// Sets the API key authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="apiKeyOptions">The API key options to set.</param>
    public static ScalarOpenApiOptions WithApiKeyAuthentication(this ScalarOpenApiOptions options, ApiKeyOptions apiKeyOptions)
    {
        options.Authentication ??= new ScalarOpenApiAuthenticationOptions();
        options.Authentication.ApiKey = apiKeyOptions;
        return options;
    }

    /// <summary>
    /// Configures the API key authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="configureApiKeyOptions">The action to configure the API key options.</param>
    public static ScalarOpenApiOptions WithApiKeyAuthentication(this ScalarOpenApiOptions options, Action<ApiKeyOptions> configureApiKeyOptions)
    {
        var apiKeyOptions = new ApiKeyOptions();
        configureApiKeyOptions(apiKeyOptions);
        return options.WithApiKeyAuthentication(apiKeyOptions);
    }

    /// <summary>
    /// Configures the OAuth2 authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="oauth2Options">The OAuth2 options to set.</param>
    public static ScalarOpenApiOptions WithOAuth2Authentication(this ScalarOpenApiOptions options, OAuth2Options oauth2Options)
    {
        options.Authentication ??= new ScalarOpenApiAuthenticationOptions();
        options.Authentication.OAuth2 = oauth2Options;
        return options;
    }

    /// <summary>
    /// Configures the OAuth2 authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="configureOAuth2Options">The action to configure the OAuth2 options.</param>
    public static ScalarOpenApiOptions WithOAuth2Authentication(this ScalarOpenApiOptions options, Action<OAuth2Options> configureOAuth2Options)
    {
        var oauth2Options = new OAuth2Options();
        configureOAuth2Options(oauth2Options);
        return options.WithOAuth2Authentication(oauth2Options);
    }

    /// <summary>
    /// Sets the default HTTP client.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="targets">The target to set.</param>
    /// <param name="clients">The client to set.</param>
    public static ScalarOpenApiOptions WithDefaultHttpClient(this ScalarOpenApiOptions options, ScalarOpenApiTargets targets, ScalarOpenApiClients clients)
    {
        options.DefaultHttpClient = new KeyValuePair<ScalarOpenApiTargets, ScalarOpenApiClients>(targets, clients);
        return options;
    }

    /// <summary>
    /// Sets the route pattern of the OpenAPI document.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="pattern">The route pattern to set.</param>
    public static ScalarOpenApiOptions WithOpenApiRoutePattern(this ScalarOpenApiOptions options, [StringSyntax("Route")] string pattern)
    {
        options.OpenApiRoutePattern = pattern;
        return options;
    }

    /// <summary>
    /// Sets the CDN URL for the API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOpenApiOptions" />.</param>
    /// <param name="url">The CDN URL to set.</param>
    public static ScalarOpenApiOptions WithCdnUrl(this ScalarOpenApiOptions options, string url)
    {
        options.CdnUrl = url;
        return options;
    }
}