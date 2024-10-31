using NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Mapper;

internal static class ScalarOpenApiOptionsMapper
{
    internal static readonly Dictionary<ScalarOpenApiTargets, ScalarOpenApiClients[]> ClientOptions = new()
    {
        { ScalarOpenApiTargets.C, [ScalarOpenApiClients.Libcurl] },
        { ScalarOpenApiTargets.Clojure, [ScalarOpenApiClients.CljHttp] },
        { ScalarOpenApiTargets.CSharp, [ScalarOpenApiClients.HttpClient, ScalarOpenApiClients.RestSharp] },
        { ScalarOpenApiTargets.Http, [ScalarOpenApiClients.Http11] },
        { ScalarOpenApiTargets.Java, [ScalarOpenApiClients.AsyncHttp, ScalarOpenApiClients.NetHttp, ScalarOpenApiClients.OkHttp, ScalarOpenApiClients.Unirest] },
        { ScalarOpenApiTargets.JavaScript, [ScalarOpenApiClients.Xhr, ScalarOpenApiClients.Axios, ScalarOpenApiClients.Fetch, ScalarOpenApiClients.JQuery] },
        { ScalarOpenApiTargets.Node, [ScalarOpenApiClients.Undici, ScalarOpenApiClients.Native, ScalarOpenApiClients.Request, ScalarOpenApiClients.Unirest, ScalarOpenApiClients.Axios, ScalarOpenApiClients.Fetch] },
        { ScalarOpenApiTargets.ObjC, [ScalarOpenApiClients.NsUrlSession] },
        { ScalarOpenApiTargets.OCaml, [ScalarOpenApiClients.CoHttp] },
        { ScalarOpenApiTargets.Php, [ScalarOpenApiClients.Curl, ScalarOpenApiClients.Guzzle, ScalarOpenApiClients.Http1, ScalarOpenApiClients.Http2] },
        { ScalarOpenApiTargets.PowerShell, [ScalarOpenApiClients.WebRequest, ScalarOpenApiClients.RestMethod] },
        { ScalarOpenApiTargets.Python, [ScalarOpenApiClients.Python3, ScalarOpenApiClients.Requests] },
        { ScalarOpenApiTargets.R, [ScalarOpenApiClients.Httr] },
        { ScalarOpenApiTargets.Ruby, [ScalarOpenApiClients.Native] },
        { ScalarOpenApiTargets.Shell, [ScalarOpenApiClients.Curl, ScalarOpenApiClients.Httpie, ScalarOpenApiClients.Wget] },
        { ScalarOpenApiTargets.Swift, [ScalarOpenApiClients.NsUrlSession] },
        { ScalarOpenApiTargets.Go, [ScalarOpenApiClients.Native] },
        { ScalarOpenApiTargets.Kotlin, [ScalarOpenApiClients.OkHttp] }
    };

    internal static ScalarOpenApiConfiguration ToOpenApiScalarConfiguration(this ScalarOpenApiOptions options)
    {
        return new ScalarOpenApiConfiguration
        {
            Proxy = options.ProxyUrl,
            Theme = options.Themes.GetValue(),
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

    private static Dictionary<ScalarOpenApiTargets, ScalarOpenApiClients[]>? ProcessOptions(ScalarOpenApiOptions options)
    {
        if (options.HiddenClients)
        {
            return ClientOptions;
        }

        if (options.EnabledTargets.Length == 0 && options.EnabledClients.Length == 0)
        {
            return null;
        }

        var selected = new Dictionary<ScalarOpenApiTargets, ScalarOpenApiClients[]>();
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