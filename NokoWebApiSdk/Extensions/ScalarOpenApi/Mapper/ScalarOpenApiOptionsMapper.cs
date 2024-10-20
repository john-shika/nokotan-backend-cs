using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Mapper;

internal static class ScalarOpenApiOptionsMapper
{
    internal static readonly Dictionary<ScalarOpenApiTarget, ScalarOpenApiClient[]> ClientOptions = new()
    {
        { ScalarOpenApiTarget.C, [ScalarOpenApiClient.Libcurl] },
        { ScalarOpenApiTarget.Clojure, [ScalarOpenApiClient.CljHttp] },
        { ScalarOpenApiTarget.CSharp, [ScalarOpenApiClient.HttpClient, ScalarOpenApiClient.RestSharp] },
        { ScalarOpenApiTarget.Http, [ScalarOpenApiClient.Http11] },
        { ScalarOpenApiTarget.Java, [ScalarOpenApiClient.AsyncHttp, ScalarOpenApiClient.NetHttp, ScalarOpenApiClient.OkHttp, ScalarOpenApiClient.Unirest] },
        { ScalarOpenApiTarget.JavaScript, [ScalarOpenApiClient.Xhr, ScalarOpenApiClient.Axios, ScalarOpenApiClient.Fetch, ScalarOpenApiClient.JQuery] },
        { ScalarOpenApiTarget.Node, [ScalarOpenApiClient.Undici, ScalarOpenApiClient.Native, ScalarOpenApiClient.Request, ScalarOpenApiClient.Unirest, ScalarOpenApiClient.Axios, ScalarOpenApiClient.Fetch] },
        { ScalarOpenApiTarget.ObjC, [ScalarOpenApiClient.NsUrlSession] },
        { ScalarOpenApiTarget.OCaml, [ScalarOpenApiClient.CoHttp] },
        { ScalarOpenApiTarget.Php, [ScalarOpenApiClient.Curl, ScalarOpenApiClient.Guzzle, ScalarOpenApiClient.Http1, ScalarOpenApiClient.Http2] },
        { ScalarOpenApiTarget.PowerShell, [ScalarOpenApiClient.WebRequest, ScalarOpenApiClient.RestMethod] },
        { ScalarOpenApiTarget.Python, [ScalarOpenApiClient.Python3, ScalarOpenApiClient.Requests] },
        { ScalarOpenApiTarget.R, [ScalarOpenApiClient.Httr] },
        { ScalarOpenApiTarget.Ruby, [ScalarOpenApiClient.Native] },
        { ScalarOpenApiTarget.Shell, [ScalarOpenApiClient.Curl, ScalarOpenApiClient.Httpie, ScalarOpenApiClient.Wget] },
        { ScalarOpenApiTarget.Swift, [ScalarOpenApiClient.NsUrlSession] },
        { ScalarOpenApiTarget.Go, [ScalarOpenApiClient.Native] },
        { ScalarOpenApiTarget.Kotlin, [ScalarOpenApiClient.OkHttp] }
    };

    internal static ScalarOpenApiConfiguration ToOpenApiScalarConfiguration(this ScalarOpenApiOptions options)
    {
        return new ScalarOpenApiConfiguration
        {
            Proxy = options.ProxyUrl,
            Theme = options.Theme.GetValue(),
            Favicon = options.Favicon,
            DarkMode = options.DarkMode,
            HideModels = options.HideModels,
            HideDarkModeToggle = options.HideDarkModeToggle,
            HideDownloadButton = options.HideDownloadButton,
            HideTestRequestButton = options.HideTestRequestButton,
            DefaultOpenAllTags = options.DefaultOpenAllTags,
            ForceDarkModeState = options.ForceThemeMode?.GetValue(),
            ShowSidebar = options.ShowSidebar,
            DefaultFonts = options.WithDefaultFonts,
            CustomCss = options.CustomCss,
            SearchHotKey = options.SearchHotKey,
            Servers = options.Servers,
            Metadata = options.Metadata,
            Authentication = options.Authentication,
            TagSorter = options.TagSorter?.GetValue(),
            HiddenClients = GetHiddenClients(options),
            DefaultHttpClient = new DefaultHttpClient
            {
                ClientKey = options.DefaultHttpClient.Value.GetValue(),
                TargetKey = options.DefaultHttpClient.Key.GetValue()
            }
        };
    }

    private static Dictionary<string, IEnumerable<string>>? GetHiddenClients(ScalarOpenApiOptions options)
    {
        var targets = ProcessOptions(options);

        return targets?.ToDictionary(k =>
                k.Key.ToString(),
            k => k.Value.Select(v => v.ToString())
        );
    }

    private static Dictionary<ScalarOpenApiTarget, ScalarOpenApiClient[]>? ProcessOptions(ScalarOpenApiOptions options)
    {
        if (options.HiddenClients)
        {
            return ClientOptions;
        }

        if (options.EnabledTargets.Length == 0 && options.EnabledClients.Length == 0)
        {
            return null;
        }

        var selected = new Dictionary<ScalarOpenApiTarget, ScalarOpenApiClient[]>();
        foreach (var item in ClientOptions)
        {
            if (options.EnabledTargets.Length != 0 &&
                !options.EnabledTargets.Contains(item.Key))
            {
                selected.Add(item.Key, item.Value);
                continue;
            }

            if (options.EnabledClients.Length == 0)
            {
                continue;
            }

            var clients = item.Value
                .Where(client => !options.EnabledClients.Contains(client))
                .ToArray();

            if (clients.Length != 0)
            {
                selected.Add(item.Key, clients);
            }
        }
        
        return selected;
    }
}