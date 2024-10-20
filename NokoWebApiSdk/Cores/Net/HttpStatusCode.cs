using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Net;

public enum HttpStatusCode
{
    Continue = 100,
    SwitchingProtocols = 101,
    Processing = 102,
    EarlyHints = 103,
    Ok = 200,
    Created = 201,
    Accepted = 202,
    NonAuthoritativeInformation = 203,
    NoContent = 204,
    ResetContent = 205,
    PartialContent = 206,
    MultiStatus = 207,
    AlreadyReported = 208,
    ImUsed = 226,
    MultipleChoices = 300,
    MovedPermanently = 301,
    Found = 302,
    SeeOther = 303,
    NotModified = 304,
    UseProxy = 305,
    Unused = 306,
    TemporaryRedirect = 307,
    PermanentRedirect = 308,
    BadRequest = 400,
    Unauthorized = 401,
    PaymentRequired = 402,
    Forbidden = 403,
    NotFound = 404,
    MethodNotAllowed = 405,
    NotAcceptable = 406,
    ProxyAuthenticationRequired = 407,
    RequestTimeout = 408,
    Conflict = 409,
    Gone = 410,
    LengthRequired = 411,
    PreconditionFailed = 412,
    PayloadTooLarge = 413,
    RequestUriTooLong = 414,
    UnsupportedMediaType = 415,
    RequestedRangeNotSatisfiable = 416,
    ExpectationFailed = 417,
    ImATeapot = 418,
    InsufficientSpaceOnResource = 419,
    MethodFailure = 420,
    MisdirectedRequest = 421,
    UnprocessableEntity = 422,
    Locked = 423,
    FailedDependency = 424,
    UpgradeRequired = 426,
    PreconditionRequired = 428,
    TooManyRequests = 429,
    RequestHeaderFieldsTooLarge = 431,
    UnavailableForLegalReasons = 451,
    InternalServerError = 500,
    NotImplemented = 501,
    BadGateway = 502,
    ServiceUnavailable = 503,
    GatewayTimeout = 504,
    HttpVersionNotSupported = 505,
    VariantAlsoNegotiates = 506,
    InsufficientStorage = 507,
    LoopDetected = 508,
    NotExtended = 510,
    NetworkAuthenticationRequired = 511,
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public sealed class HttpStatusCodeValues
{
    public const string Continue = "CONTINUE";
    public const string SwitchingProtocols = "SWITCHING_PROTOCOLS";
    public const string Processing = "PROCESSING";
    public const string EarlyHints = "EARLY_HINTS";
    public const string Ok = "OK";
    public const string Created = "CREATED";
    public const string Accepted = "ACCEPTED";
    public const string NonAuthoritativeInformation = "NON_AUTHORITATIVE_INFORMATION";
    public const string NoContent = "NO_CONTENT";
    public const string ResetContent = "RESET_CONTENT";
    public const string PartialContent = "PARTIAL_CONTENT";
    public const string MultiStatus = "MULTI_STATUS";
    public const string AlreadyReported = "ALREADY_REPORTED";
    public const string ImUsed = "IM_USED";
    public const string MultipleChoices = "MULTI_CHOICES";
    public const string MovedPermanently = "MOVED_PERMANENTLY";
    public const string Found = "FOUND";
    public const string SeeOther = "SEE_OTHER";
    public const string NotModified = "NOT_MODIFIED";
    public const string UseProxy = "USE_PROXY";
    public const string Unused = "UNUSED";
    public const string TemporaryRedirect = "TEMPORARY_REDIRECT";
    public const string PermanentRedirect = "PERMANENT_REDIRECT";
    public const string BadRequest = "BAD_REQUEST";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string PaymentRequired = "PAYMENT_REQUIRED";
    public const string Forbidden = "FORBIDDEN";
    public const string NotFound = "NOT_FOUND";
    public const string MethodNotAllowed = "METHOD_NOT_ALLOWED";
    public const string NotAcceptable = "NOT_ACCEPTABLE";
    public const string ProxyAuthenticationRequired = "PROXY_AUTHENTICATION_REQUIRED";
    public const string RequestTimeout = "REQUEST_TIMEOUT";
    public const string Conflict = "CONFLICT";
    public const string Gone = "GONE";
    public const string LengthRequired = "LENGTH_REQUIRED";
    public const string PreconditionFailed = "PRECONDITION_FAILED";
    public const string PayloadTooLarge = "PAYLOAD_TOO_LARGE";
    public const string RequestUriTooLong = "REQUEST_URI_TOO_LONG";
    public const string UnsupportedMediaType = "UNSUPPORTED_MEDIA_TYPE";
    public const string RequestedRangeNotSatisfiable = "REQUESTED_RANGE_NOT_SATISFIABLE";
    public const string ExpectationFailed = "EXPECTATION_FAILED";
    public const string ImATeapot = "IM_A_TEAPOT";
    public const string InsufficientSpaceOnResource = "INSUFFICIENT_SPACE_ON_RESOURCE";
    public const string MethodFailure = "METHOD_FAILURE";
    public const string MisdirectedRequest = "MISDIRECTED_REQUEST";
    public const string UnprocessableEntity = "UNPROCESSABLE_ENTITY";
    public const string Locked = "LOCKED";
    public const string FailedDependency = "FAILED_DEPENDENCY";
    public const string UpgradeRequired = "UPGRADE_REQUIRED";
    public const string PreconditionRequired = "PRECONDITION_REQUIRED";
    public const string TooManyRequests = "TOO_MANY_REQUESTS";
    public const string RequestHeaderFieldsTooLarge = "REQUEST_HEADER_FIELDS_TOO_LARGE";
    public const string UnavailableForLegalReasons = "UNAVAILABLE_FOR_LEGAL_REASONS";
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";
    public const string NotImplemented = "NOT_IMPLEMENTED";
    public const string BadGateway = "BAD_GATEWAY";
    public const string ServiceUnavailable = "SERVICE_UNAVAILABLE";
    public const string GatewayTimeout = "GATEWAY_TIMEOUT";
    public const string HttpVersionNotSupported = "HTTP_VERSION_NOT_SUPPORTED";
    public const string VariantAlsoNegotiates = "VARIANT_ALSO_NEGOTIATES";
    public const string InsufficientStorage = "INSUFFICIENT_STORAGE";
    public const string LoopDetected = "LOOP_DETECTED";
    public const string NotExtended = "NOT_EXTENDED";
    public const string NetworkAuthenticationRequired = "NETWORK_AUTHENTICATION_REQUIRED";
    
    public static HttpStatusCode ParseCode(string code)
    {
        return NokoWebTransformText.ToSnakeCaseUpper(code) switch
        {
            Continue => HttpStatusCode.Continue,
            SwitchingProtocols => HttpStatusCode.SwitchingProtocols,
            Processing => HttpStatusCode.Processing,
            EarlyHints => HttpStatusCode.EarlyHints,
            Ok => HttpStatusCode.Ok,
            Created => HttpStatusCode.Created,
            Accepted => HttpStatusCode.Accepted,
            NonAuthoritativeInformation => HttpStatusCode.NonAuthoritativeInformation,
            NoContent => HttpStatusCode.NoContent,
            ResetContent => HttpStatusCode.ResetContent,
            PartialContent => HttpStatusCode.PartialContent,
            MultiStatus => HttpStatusCode.MultiStatus,
            AlreadyReported => HttpStatusCode.AlreadyReported,
            ImUsed => HttpStatusCode.ImUsed,
            MultipleChoices => HttpStatusCode.MultipleChoices,
            MovedPermanently => HttpStatusCode.MovedPermanently,
            Found => HttpStatusCode.Found,
            SeeOther => HttpStatusCode.SeeOther,
            NotModified => HttpStatusCode.NotModified,
            UseProxy => HttpStatusCode.UseProxy,
            Unused => HttpStatusCode.Unused,
            TemporaryRedirect => HttpStatusCode.TemporaryRedirect,
            PermanentRedirect => HttpStatusCode.PermanentRedirect,
            BadRequest => HttpStatusCode.BadRequest,
            Unauthorized => HttpStatusCode.Unauthorized,
            PaymentRequired => HttpStatusCode.PaymentRequired,
            Forbidden => HttpStatusCode.Forbidden,
            NotFound => HttpStatusCode.NotFound,
            MethodNotAllowed => HttpStatusCode.MethodNotAllowed,
            NotAcceptable => HttpStatusCode.NotAcceptable,
            ProxyAuthenticationRequired => HttpStatusCode.ProxyAuthenticationRequired,
            RequestTimeout => HttpStatusCode.RequestTimeout,
            Conflict => HttpStatusCode.Conflict,
            Gone => HttpStatusCode.Gone,
            LengthRequired => HttpStatusCode.LengthRequired,
            PreconditionFailed => HttpStatusCode.PreconditionFailed,
            PayloadTooLarge => HttpStatusCode.PayloadTooLarge,
            RequestUriTooLong => HttpStatusCode.RequestUriTooLong,
            UnsupportedMediaType => HttpStatusCode.UnsupportedMediaType,
            RequestedRangeNotSatisfiable => HttpStatusCode.RequestedRangeNotSatisfiable,
            ExpectationFailed => HttpStatusCode.ExpectationFailed,
            ImATeapot => HttpStatusCode.ImATeapot,
            InsufficientSpaceOnResource => HttpStatusCode.InsufficientSpaceOnResource,
            MethodFailure => HttpStatusCode.MethodFailure,
            MisdirectedRequest => HttpStatusCode.MisdirectedRequest,
            UnprocessableEntity => HttpStatusCode.UnprocessableEntity,
            Locked => HttpStatusCode.Locked,
            FailedDependency => HttpStatusCode.FailedDependency,
            UpgradeRequired => HttpStatusCode.UpgradeRequired,
            PreconditionRequired => HttpStatusCode.PreconditionRequired,
            TooManyRequests => HttpStatusCode.TooManyRequests,
            RequestHeaderFieldsTooLarge => HttpStatusCode.RequestHeaderFieldsTooLarge,
            UnavailableForLegalReasons => HttpStatusCode.UnavailableForLegalReasons,
            InternalServerError => HttpStatusCode.InternalServerError,
            NotImplemented => HttpStatusCode.NotImplemented,
            BadGateway => HttpStatusCode.BadGateway,
            ServiceUnavailable => HttpStatusCode.ServiceUnavailable,
            GatewayTimeout => HttpStatusCode.GatewayTimeout,
            HttpVersionNotSupported => HttpStatusCode.HttpVersionNotSupported,
            VariantAlsoNegotiates => HttpStatusCode.VariantAlsoNegotiates,
            InsufficientStorage => HttpStatusCode.InsufficientStorage,
            LoopDetected => HttpStatusCode.LoopDetected,
            NotExtended => HttpStatusCode.NotExtended,
            NetworkAuthenticationRequired => HttpStatusCode.NetworkAuthenticationRequired,
            _ => throw new FormatException($"Invalid status code {code}"),
        };
    }

    public static string FromCode(HttpStatusCode code)
    {
        return code switch
        {
            HttpStatusCode.Continue => Continue,
            HttpStatusCode.SwitchingProtocols => SwitchingProtocols,
            HttpStatusCode.Processing => Processing,
            HttpStatusCode.EarlyHints => EarlyHints,
            HttpStatusCode.Ok => Ok,
            HttpStatusCode.Created => Created,
            HttpStatusCode.Accepted => Accepted,
            HttpStatusCode.NonAuthoritativeInformation => NonAuthoritativeInformation,
            HttpStatusCode.NoContent => NoContent,
            HttpStatusCode.ResetContent => ResetContent,
            HttpStatusCode.PartialContent => PartialContent,
            HttpStatusCode.MultiStatus => MultiStatus,
            HttpStatusCode.AlreadyReported => AlreadyReported,
            HttpStatusCode.ImUsed => ImUsed,
            HttpStatusCode.MultipleChoices => MultipleChoices,
            HttpStatusCode.MovedPermanently => MovedPermanently,
            HttpStatusCode.Found => Found,
            HttpStatusCode.SeeOther => SeeOther,
            HttpStatusCode.NotModified => NotModified,
            HttpStatusCode.UseProxy => UseProxy,
            HttpStatusCode.Unused => Unused,
            HttpStatusCode.TemporaryRedirect => TemporaryRedirect,
            HttpStatusCode.PermanentRedirect => PermanentRedirect,
            HttpStatusCode.BadRequest => BadRequest,
            HttpStatusCode.Unauthorized => Unauthorized,
            HttpStatusCode.PaymentRequired => PaymentRequired,
            HttpStatusCode.Forbidden => Forbidden,
            HttpStatusCode.NotFound => NotFound,
            HttpStatusCode.MethodNotAllowed => MethodNotAllowed,
            HttpStatusCode.NotAcceptable => NotAcceptable,
            HttpStatusCode.ProxyAuthenticationRequired => ProxyAuthenticationRequired,
            HttpStatusCode.RequestTimeout => RequestTimeout,
            HttpStatusCode.Conflict => Conflict,
            HttpStatusCode.Gone => Gone,
            HttpStatusCode.LengthRequired => LengthRequired,
            HttpStatusCode.PreconditionFailed => PreconditionFailed,
            HttpStatusCode.PayloadTooLarge => PayloadTooLarge,
            HttpStatusCode.RequestUriTooLong => RequestUriTooLong,
            HttpStatusCode.UnsupportedMediaType => UnsupportedMediaType,
            HttpStatusCode.RequestedRangeNotSatisfiable => RequestedRangeNotSatisfiable,
            HttpStatusCode.ExpectationFailed => ExpectationFailed,
            HttpStatusCode.ImATeapot => ImATeapot,
            HttpStatusCode.InsufficientSpaceOnResource => InsufficientSpaceOnResource,
            HttpStatusCode.MethodFailure => MethodFailure,
            HttpStatusCode.MisdirectedRequest => MisdirectedRequest,
            HttpStatusCode.UnprocessableEntity => UnprocessableEntity,
            HttpStatusCode.Locked => Locked,
            HttpStatusCode.FailedDependency => FailedDependency,
            HttpStatusCode.UpgradeRequired => UpgradeRequired,
            HttpStatusCode.PreconditionRequired => PreconditionRequired,
            HttpStatusCode.TooManyRequests => TooManyRequests,
            HttpStatusCode.RequestHeaderFieldsTooLarge => RequestHeaderFieldsTooLarge,
            HttpStatusCode.UnavailableForLegalReasons => UnavailableForLegalReasons,
            HttpStatusCode.InternalServerError => InternalServerError,
            HttpStatusCode.NotImplemented => NotImplemented,
            HttpStatusCode.BadGateway => BadGateway,
            HttpStatusCode.ServiceUnavailable => ServiceUnavailable,
            HttpStatusCode.GatewayTimeout => GatewayTimeout,
            HttpStatusCode.HttpVersionNotSupported => HttpVersionNotSupported,
            HttpStatusCode.VariantAlsoNegotiates => VariantAlsoNegotiates,
            HttpStatusCode.InsufficientStorage => InsufficientStorage,
            HttpStatusCode.LoopDetected => LoopDetected,
            HttpStatusCode.NotExtended => NotExtended,
            HttpStatusCode.NetworkAuthenticationRequired => NetworkAuthenticationRequired,
            _ => throw new Exception($"Unsupported status code {code}"),
        };
    }
}

public static class HttpStatusCodeExtensions
{
    public static string GetKey(this HttpStatusCode code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this HttpStatusCode code)
    {
        return HttpStatusCodeValues.FromCode(code);
    }
}

public class JsonHttpStatusCodeSerializeConverter : JsonConverter<HttpStatusCode>
{
    public override HttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, HttpStatusCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
