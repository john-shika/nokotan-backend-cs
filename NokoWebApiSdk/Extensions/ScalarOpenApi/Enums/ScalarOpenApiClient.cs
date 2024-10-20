using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Extensions.ScalarOpenApi.Enums;

public enum ScalarOpenApiClient
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

[StructLayout(LayoutKind.Sequential, Size = 1)]
public sealed class ScalarOpenApiClientValues
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

    public static ScalarOpenApiClient ParseCode(string code)
    {
        return (NokoWebTransformText.ToSnakeCase(code)) switch
        {
            Libcurl => ScalarOpenApiClient.Libcurl,
            CljHttp => ScalarOpenApiClient.CljHttp,
            HttpClient => ScalarOpenApiClient.HttpClient,
            RestSharp => ScalarOpenApiClient.RestSharp,
            Native => ScalarOpenApiClient.Native,
            Http11 => ScalarOpenApiClient.Http11,
            AsyncHttp => ScalarOpenApiClient.AsyncHttp,
            NetHttp => ScalarOpenApiClient.NetHttp,
            OkHttp => ScalarOpenApiClient.OkHttp,
            Unirest => ScalarOpenApiClient.Unirest,
            Xhr => ScalarOpenApiClient.Xhr,
            Axios => ScalarOpenApiClient.Axios,
            Fetch => ScalarOpenApiClient.Fetch,
            JQuery => ScalarOpenApiClient.JQuery,
            Undici => ScalarOpenApiClient.Undici,
            Request => ScalarOpenApiClient.Request,
            NsUrlSession => ScalarOpenApiClient.NsUrlSession,
            CoHttp => ScalarOpenApiClient.CoHttp,
            Curl => ScalarOpenApiClient.Curl,
            Guzzle => ScalarOpenApiClient.Guzzle,
            Http1 => ScalarOpenApiClient.Http1,
            Http2 => ScalarOpenApiClient.Http2,
            WebRequest => ScalarOpenApiClient.WebRequest,
            RestMethod => ScalarOpenApiClient.RestMethod,
            Python3 => ScalarOpenApiClient.Python3,
            Requests => ScalarOpenApiClient.Requests,
            Httr => ScalarOpenApiClient.Httr,
            Httpie => ScalarOpenApiClient.Httpie,
            Wget => ScalarOpenApiClient.Wget,
            _ => throw new FormatException($"Invalid client code {code}"),
        };
    }

    public static string FromCode(ScalarOpenApiClient code)
    {
        return (code) switch
        {
            ScalarOpenApiClient.Libcurl => Libcurl,
            ScalarOpenApiClient.CljHttp => CljHttp,
            ScalarOpenApiClient.HttpClient => HttpClient,
            ScalarOpenApiClient.RestSharp => RestSharp,
            ScalarOpenApiClient.Native => Native,
            ScalarOpenApiClient.Http11 => Http11,
            ScalarOpenApiClient.AsyncHttp => AsyncHttp,
            ScalarOpenApiClient.NetHttp => NetHttp,
            ScalarOpenApiClient.OkHttp => OkHttp,
            ScalarOpenApiClient.Unirest => Unirest,
            ScalarOpenApiClient.Xhr => Xhr,
            ScalarOpenApiClient.Axios => Axios,
            ScalarOpenApiClient.Fetch => Fetch,
            ScalarOpenApiClient.JQuery => JQuery,
            ScalarOpenApiClient.Undici => Undici,
            ScalarOpenApiClient.Request => Request,
            ScalarOpenApiClient.NsUrlSession => NsUrlSession,
            ScalarOpenApiClient.CoHttp => CoHttp,
            ScalarOpenApiClient.Curl => Curl,
            ScalarOpenApiClient.Guzzle => Guzzle,
            ScalarOpenApiClient.Http1 => Http1,
            ScalarOpenApiClient.Http2 => Http2,
            ScalarOpenApiClient.WebRequest => WebRequest,
            ScalarOpenApiClient.RestMethod => RestMethod,
            ScalarOpenApiClient.Python3 => Python3,
            ScalarOpenApiClient.Requests => Requests,
            ScalarOpenApiClient.Httr => Httr,
            ScalarOpenApiClient.Httpie => Httpie,
            ScalarOpenApiClient.Wget => Wget,
            _ => throw new Exception($"Unsupported client code {code}"),
        };
    }
}

public static class ScalarOpenApiClientExtensions
{
    public static string GetKey(this ScalarOpenApiClient code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }
    
    public static string GetValue(this ScalarOpenApiClient code)
    {
        return ScalarOpenApiClientValues.FromCode(code);
    }
}

public class ScalarOpenApiClientSerializeConverter : JsonConverter<ScalarOpenApiClient>
{
    public override ScalarOpenApiClient Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ScalarOpenApiClient value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
