using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiClients
{
    Libcurl,
    CljHttp,
    HttpClient,
    RestSharp,
    Native,
    Http11,
    AsyncHttp,
    NetHttp,
    OkHttp,
    Unirest,
    Xhr,
    Axios,
    Fetch,
    JQuery,
    Undici,
    Request,
    NsUrlSession,
    CoHttp,
    Curl,
    Guzzle,
    Http1,
    Http2,
    WebRequest,
    RestMethod,
    Python3,
    Requests,
    Httr,
    Httpie,
    Wget,
}

public static class ScalarOpenApiClientValues
{
    public const string Libcurl = "libcurl";
    public const string CljHttp = "clj_http";
    public const string HttpClient = "httpclient";
    public const string RestSharp = "restsharp";
    public const string Native = "native";
    public const string Http11 = "http1.1";
    public const string AsyncHttp = "asynchttp";
    public const string NetHttp = "nethttp";
    public const string OkHttp = "okhttp";
    public const string Unirest = "unirest";
    public const string Xhr = "xhr";
    public const string Axios = "axios";
    public const string Fetch = "fetch";
    public const string JQuery = "jquery";
    public const string Undici = "undici";
    public const string Request = "request";
    public const string NsUrlSession = "nsurlsession";
    public const string CoHttp = "cohttp";
    public const string Curl = "curl";
    public const string Guzzle = "guzzle";
    public const string Http1 = "http1";
    public const string Http2 = "http2";
    public const string WebRequest = "webrequest";
    public const string RestMethod = "restmethod";
    public const string Python3 = "python3";
    public const string Requests = "requests";
    public const string Httr = "httr";
    public const string Httpie = "httpie";
    public const string Wget = "wget";

    public static ScalarOpenApiClients ParseCode(string code)
    {
        return (NokoTransformText.ToSnakeCase(code)) switch
        {
            Libcurl => ScalarOpenApiClients.Libcurl,
            CljHttp => ScalarOpenApiClients.CljHttp,
            HttpClient => ScalarOpenApiClients.HttpClient,
            RestSharp => ScalarOpenApiClients.RestSharp,
            Native => ScalarOpenApiClients.Native,
            Http11 => ScalarOpenApiClients.Http11,
            AsyncHttp => ScalarOpenApiClients.AsyncHttp,
            NetHttp => ScalarOpenApiClients.NetHttp,
            OkHttp => ScalarOpenApiClients.OkHttp,
            Unirest => ScalarOpenApiClients.Unirest,
            Xhr => ScalarOpenApiClients.Xhr,
            Axios => ScalarOpenApiClients.Axios,
            Fetch => ScalarOpenApiClients.Fetch,
            JQuery => ScalarOpenApiClients.JQuery,
            Undici => ScalarOpenApiClients.Undici,
            Request => ScalarOpenApiClients.Request,
            NsUrlSession => ScalarOpenApiClients.NsUrlSession,
            CoHttp => ScalarOpenApiClients.CoHttp,
            Curl => ScalarOpenApiClients.Curl,
            Guzzle => ScalarOpenApiClients.Guzzle,
            Http1 => ScalarOpenApiClients.Http1,
            Http2 => ScalarOpenApiClients.Http2,
            WebRequest => ScalarOpenApiClients.WebRequest,
            RestMethod => ScalarOpenApiClients.RestMethod,
            Python3 => ScalarOpenApiClients.Python3,
            Requests => ScalarOpenApiClients.Requests,
            Httr => ScalarOpenApiClients.Httr,
            Httpie => ScalarOpenApiClients.Httpie,
            Wget => ScalarOpenApiClients.Wget,
            _ => throw new FormatException($"Invalid client code {code}"),
        };
    }

    public static string FromCode(ScalarOpenApiClients code)
    {
        return (code) switch
        {
            ScalarOpenApiClients.Libcurl => Libcurl,
            ScalarOpenApiClients.CljHttp => CljHttp,
            ScalarOpenApiClients.HttpClient => HttpClient,
            ScalarOpenApiClients.RestSharp => RestSharp,
            ScalarOpenApiClients.Native => Native,
            ScalarOpenApiClients.Http11 => Http11,
            ScalarOpenApiClients.AsyncHttp => AsyncHttp,
            ScalarOpenApiClients.NetHttp => NetHttp,
            ScalarOpenApiClients.OkHttp => OkHttp,
            ScalarOpenApiClients.Unirest => Unirest,
            ScalarOpenApiClients.Xhr => Xhr,
            ScalarOpenApiClients.Axios => Axios,
            ScalarOpenApiClients.Fetch => Fetch,
            ScalarOpenApiClients.JQuery => JQuery,
            ScalarOpenApiClients.Undici => Undici,
            ScalarOpenApiClients.Request => Request,
            ScalarOpenApiClients.NsUrlSession => NsUrlSession,
            ScalarOpenApiClients.CoHttp => CoHttp,
            ScalarOpenApiClients.Curl => Curl,
            ScalarOpenApiClients.Guzzle => Guzzle,
            ScalarOpenApiClients.Http1 => Http1,
            ScalarOpenApiClients.Http2 => Http2,
            ScalarOpenApiClients.WebRequest => WebRequest,
            ScalarOpenApiClients.RestMethod => RestMethod,
            ScalarOpenApiClients.Python3 => Python3,
            ScalarOpenApiClients.Requests => Requests,
            ScalarOpenApiClients.Httr => Httr,
            ScalarOpenApiClients.Httpie => Httpie,
            ScalarOpenApiClients.Wget => Wget,
            _ => throw new Exception($"Unsupported client code {code}"),
        };
    }
}

public static class ScalarOpenApiClientExtensions
{
    public static string GetKey(this ScalarOpenApiClients code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiClients code)
    {
        return ScalarOpenApiClientValues.FromCode(code);
    }
}

public class ScalarOpenApiClientSerializeConverter : JsonConverter<ScalarOpenApiClients>
{
    public override ScalarOpenApiClients Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiClients value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
