using NokoWebApiSdk.Extensions.OpenApi.Scalar.Enums;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Options;
using NokoWebApiSdk.Extensions.ScalarOpenApi.Schemas;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Mapper;

internal static class ScalarOpenApiOptionsMapper
{
    internal static readonly Dictionary<OpenApiScalarTarget, OpenApiScalarClient[]> ClientOptions = new()
    {
        { OpenApiScalarTarget.C, [OpenApiScalarClient.Libcurl] },
        { OpenApiScalarTarget.Clojure, [OpenApiScalarClient.CljHttp] },
        { OpenApiScalarTarget.CSharp, [OpenApiScalarClient.HttpClient, OpenApiScalarClient.RestSharp] },
        { OpenApiScalarTarget.Http, [OpenApiScalarClient.Http11] },
        { OpenApiScalarTarget.Java, [OpenApiScalarClient.AsyncHttp, OpenApiScalarClient.NetHttp, OpenApiScalarClient.OkHttp, OpenApiScalarClient.Unirest] },
        { OpenApiScalarTarget.JavaScript, [OpenApiScalarClient.Xhr, OpenApiScalarClient.Axios, OpenApiScalarClient.Fetch, OpenApiScalarClient.JQuery] },
        { OpenApiScalarTarget.Node, [OpenApiScalarClient.Undici, OpenApiScalarClient.Native, OpenApiScalarClient.Request, OpenApiScalarClient.Unirest, OpenApiScalarClient.Axios, OpenApiScalarClient.Fetch] },
        { OpenApiScalarTarget.ObjC, [OpenApiScalarClient.Nsurlsession] },
        { OpenApiScalarTarget.OCaml, [OpenApiScalarClient.CoHttp] },
        { OpenApiScalarTarget.Php, [OpenApiScalarClient.Curl, OpenApiScalarClient.Guzzle, OpenApiScalarClient.Http1, OpenApiScalarClient.Http2] },
        { OpenApiScalarTarget.PowerShell, [OpenApiScalarClient.WebRequest, OpenApiScalarClient.RestMethod] },
        { OpenApiScalarTarget.Python, [OpenApiScalarClient.Python3, OpenApiScalarClient.Requests] },
        { OpenApiScalarTarget.R, [OpenApiScalarClient.Httr] },
        { OpenApiScalarTarget.Ruby, [OpenApiScalarClient.Native] },
        { OpenApiScalarTarget.Shell, [OpenApiScalarClient.Curl, OpenApiScalarClient.Httpie, OpenApiScalarClient.Wget] },
        { OpenApiScalarTarget.Swift, [OpenApiScalarClient.Nsurlsession] },
        { OpenApiScalarTarget.Go, [OpenApiScalarClient.Native] },
        { OpenApiScalarTarget.Kotlin, [OpenApiScalarClient.OkHttp] }
    };

    internal static ScalarOpenApiConfiguration ToOpenApiScalarConfiguration(this ScalarOpenApiOptions options)
    {
        return new ScalarOpenApiConfiguration
        {
            Proxy = options.ProxyUrl,
            Theme = options.Theme.ToString(),
            Favicon = options.Favicon,
            DarkMode = options.DarkMode,
            HideModels = options.HideModels,
            HideDarkModeToggle = options.HideDarkModeToggle,
            HideDownloadButton = options.HideDownloadButton,
            HideTestRequestButton = options.HideTestRequestButton,
            DefaultOpenAllTags = options.DefaultOpenAllTags,
            ForceDarkModeState = options.ForceThemeMode?.ToString(),
            ShowSidebar = options.ShowSidebar,
            DefaultFonts = options.WithDefaultFonts,
            CustomCss = options.CustomCss,
            SearchHotKey = options.SearchHotKey,
            Servers = options.Servers,
            Metadata = options.Metadata,
            Authentication = options.Authentication,
            TagSorter = options.TagSorter?.ToString(),
            HiddenClients = GetHiddenClients(options),
            DefaultHttpClient = new DefaultHttpClient
            {
                ClientKey = options.DefaultHttpClient.Value.ToString(),
                TargetKey = options.DefaultHttpClient.Key.ToString()
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

    private static Dictionary<OpenApiScalarTarget, OpenApiScalarClient[]>? ProcessOptions(ScalarOpenApiOptions options)
    {
        if (options.HiddenClients)
        {
            return ClientOptions;
        }

        if (options.EnabledTargets.Length == 0 && options.EnabledClients.Length == 0)
        {
            return null;
        }

        var selected = new Dictionary<OpenApiScalarTarget, OpenApiScalarClient[]>();
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