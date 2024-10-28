using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Net;

public enum NokoWebHttpStatusCode
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
public sealed class NokoWebHttpStatusCodeValues
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
    
    public static NokoWebHttpStatusCode ParseCode(string code)
    {
        return NokoWebTransformText.ToSnakeCaseUpper(code) switch
        {
            Continue => NokoWebHttpStatusCode.Continue,
            SwitchingProtocols => NokoWebHttpStatusCode.SwitchingProtocols,
            Processing => NokoWebHttpStatusCode.Processing,
            EarlyHints => NokoWebHttpStatusCode.EarlyHints,
            Ok => NokoWebHttpStatusCode.Ok,
            Created => NokoWebHttpStatusCode.Created,
            Accepted => NokoWebHttpStatusCode.Accepted,
            NonAuthoritativeInformation => NokoWebHttpStatusCode.NonAuthoritativeInformation,
            NoContent => NokoWebHttpStatusCode.NoContent,
            ResetContent => NokoWebHttpStatusCode.ResetContent,
            PartialContent => NokoWebHttpStatusCode.PartialContent,
            MultiStatus => NokoWebHttpStatusCode.MultiStatus,
            AlreadyReported => NokoWebHttpStatusCode.AlreadyReported,
            ImUsed => NokoWebHttpStatusCode.ImUsed,
            MultipleChoices => NokoWebHttpStatusCode.MultipleChoices,
            MovedPermanently => NokoWebHttpStatusCode.MovedPermanently,
            Found => NokoWebHttpStatusCode.Found,
            SeeOther => NokoWebHttpStatusCode.SeeOther,
            NotModified => NokoWebHttpStatusCode.NotModified,
            UseProxy => NokoWebHttpStatusCode.UseProxy,
            Unused => NokoWebHttpStatusCode.Unused,
            TemporaryRedirect => NokoWebHttpStatusCode.TemporaryRedirect,
            PermanentRedirect => NokoWebHttpStatusCode.PermanentRedirect,
            BadRequest => NokoWebHttpStatusCode.BadRequest,
            Unauthorized => NokoWebHttpStatusCode.Unauthorized,
            PaymentRequired => NokoWebHttpStatusCode.PaymentRequired,
            Forbidden => NokoWebHttpStatusCode.Forbidden,
            NotFound => NokoWebHttpStatusCode.NotFound,
            MethodNotAllowed => NokoWebHttpStatusCode.MethodNotAllowed,
            NotAcceptable => NokoWebHttpStatusCode.NotAcceptable,
            ProxyAuthenticationRequired => NokoWebHttpStatusCode.ProxyAuthenticationRequired,
            RequestTimeout => NokoWebHttpStatusCode.RequestTimeout,
            Conflict => NokoWebHttpStatusCode.Conflict,
            Gone => NokoWebHttpStatusCode.Gone,
            LengthRequired => NokoWebHttpStatusCode.LengthRequired,
            PreconditionFailed => NokoWebHttpStatusCode.PreconditionFailed,
            PayloadTooLarge => NokoWebHttpStatusCode.PayloadTooLarge,
            RequestUriTooLong => NokoWebHttpStatusCode.RequestUriTooLong,
            UnsupportedMediaType => NokoWebHttpStatusCode.UnsupportedMediaType,
            RequestedRangeNotSatisfiable => NokoWebHttpStatusCode.RequestedRangeNotSatisfiable,
            ExpectationFailed => NokoWebHttpStatusCode.ExpectationFailed,
            ImATeapot => NokoWebHttpStatusCode.ImATeapot,
            InsufficientSpaceOnResource => NokoWebHttpStatusCode.InsufficientSpaceOnResource,
            MethodFailure => NokoWebHttpStatusCode.MethodFailure,
            MisdirectedRequest => NokoWebHttpStatusCode.MisdirectedRequest,
            UnprocessableEntity => NokoWebHttpStatusCode.UnprocessableEntity,
            Locked => NokoWebHttpStatusCode.Locked,
            FailedDependency => NokoWebHttpStatusCode.FailedDependency,
            UpgradeRequired => NokoWebHttpStatusCode.UpgradeRequired,
            PreconditionRequired => NokoWebHttpStatusCode.PreconditionRequired,
            TooManyRequests => NokoWebHttpStatusCode.TooManyRequests,
            RequestHeaderFieldsTooLarge => NokoWebHttpStatusCode.RequestHeaderFieldsTooLarge,
            UnavailableForLegalReasons => NokoWebHttpStatusCode.UnavailableForLegalReasons,
            InternalServerError => NokoWebHttpStatusCode.InternalServerError,
            NotImplemented => NokoWebHttpStatusCode.NotImplemented,
            BadGateway => NokoWebHttpStatusCode.BadGateway,
            ServiceUnavailable => NokoWebHttpStatusCode.ServiceUnavailable,
            GatewayTimeout => NokoWebHttpStatusCode.GatewayTimeout,
            HttpVersionNotSupported => NokoWebHttpStatusCode.HttpVersionNotSupported,
            VariantAlsoNegotiates => NokoWebHttpStatusCode.VariantAlsoNegotiates,
            InsufficientStorage => NokoWebHttpStatusCode.InsufficientStorage,
            LoopDetected => NokoWebHttpStatusCode.LoopDetected,
            NotExtended => NokoWebHttpStatusCode.NotExtended,
            NetworkAuthenticationRequired => NokoWebHttpStatusCode.NetworkAuthenticationRequired,
            _ => throw new FormatException($"Invalid status code {code}"),
        };
    }

    public static string FromCode(NokoWebHttpStatusCode code)
    {
        return code switch
        {
            NokoWebHttpStatusCode.Continue => Continue,
            NokoWebHttpStatusCode.SwitchingProtocols => SwitchingProtocols,
            NokoWebHttpStatusCode.Processing => Processing,
            NokoWebHttpStatusCode.EarlyHints => EarlyHints,
            NokoWebHttpStatusCode.Ok => Ok,
            NokoWebHttpStatusCode.Created => Created,
            NokoWebHttpStatusCode.Accepted => Accepted,
            NokoWebHttpStatusCode.NonAuthoritativeInformation => NonAuthoritativeInformation,
            NokoWebHttpStatusCode.NoContent => NoContent,
            NokoWebHttpStatusCode.ResetContent => ResetContent,
            NokoWebHttpStatusCode.PartialContent => PartialContent,
            NokoWebHttpStatusCode.MultiStatus => MultiStatus,
            NokoWebHttpStatusCode.AlreadyReported => AlreadyReported,
            NokoWebHttpStatusCode.ImUsed => ImUsed,
            NokoWebHttpStatusCode.MultipleChoices => MultipleChoices,
            NokoWebHttpStatusCode.MovedPermanently => MovedPermanently,
            NokoWebHttpStatusCode.Found => Found,
            NokoWebHttpStatusCode.SeeOther => SeeOther,
            NokoWebHttpStatusCode.NotModified => NotModified,
            NokoWebHttpStatusCode.UseProxy => UseProxy,
            NokoWebHttpStatusCode.Unused => Unused,
            NokoWebHttpStatusCode.TemporaryRedirect => TemporaryRedirect,
            NokoWebHttpStatusCode.PermanentRedirect => PermanentRedirect,
            NokoWebHttpStatusCode.BadRequest => BadRequest,
            NokoWebHttpStatusCode.Unauthorized => Unauthorized,
            NokoWebHttpStatusCode.PaymentRequired => PaymentRequired,
            NokoWebHttpStatusCode.Forbidden => Forbidden,
            NokoWebHttpStatusCode.NotFound => NotFound,
            NokoWebHttpStatusCode.MethodNotAllowed => MethodNotAllowed,
            NokoWebHttpStatusCode.NotAcceptable => NotAcceptable,
            NokoWebHttpStatusCode.ProxyAuthenticationRequired => ProxyAuthenticationRequired,
            NokoWebHttpStatusCode.RequestTimeout => RequestTimeout,
            NokoWebHttpStatusCode.Conflict => Conflict,
            NokoWebHttpStatusCode.Gone => Gone,
            NokoWebHttpStatusCode.LengthRequired => LengthRequired,
            NokoWebHttpStatusCode.PreconditionFailed => PreconditionFailed,
            NokoWebHttpStatusCode.PayloadTooLarge => PayloadTooLarge,
            NokoWebHttpStatusCode.RequestUriTooLong => RequestUriTooLong,
            NokoWebHttpStatusCode.UnsupportedMediaType => UnsupportedMediaType,
            NokoWebHttpStatusCode.RequestedRangeNotSatisfiable => RequestedRangeNotSatisfiable,
            NokoWebHttpStatusCode.ExpectationFailed => ExpectationFailed,
            NokoWebHttpStatusCode.ImATeapot => ImATeapot,
            NokoWebHttpStatusCode.InsufficientSpaceOnResource => InsufficientSpaceOnResource,
            NokoWebHttpStatusCode.MethodFailure => MethodFailure,
            NokoWebHttpStatusCode.MisdirectedRequest => MisdirectedRequest,
            NokoWebHttpStatusCode.UnprocessableEntity => UnprocessableEntity,
            NokoWebHttpStatusCode.Locked => Locked,
            NokoWebHttpStatusCode.FailedDependency => FailedDependency,
            NokoWebHttpStatusCode.UpgradeRequired => UpgradeRequired,
            NokoWebHttpStatusCode.PreconditionRequired => PreconditionRequired,
            NokoWebHttpStatusCode.TooManyRequests => TooManyRequests,
            NokoWebHttpStatusCode.RequestHeaderFieldsTooLarge => RequestHeaderFieldsTooLarge,
            NokoWebHttpStatusCode.UnavailableForLegalReasons => UnavailableForLegalReasons,
            NokoWebHttpStatusCode.InternalServerError => InternalServerError,
            NokoWebHttpStatusCode.NotImplemented => NotImplemented,
            NokoWebHttpStatusCode.BadGateway => BadGateway,
            NokoWebHttpStatusCode.ServiceUnavailable => ServiceUnavailable,
            NokoWebHttpStatusCode.GatewayTimeout => GatewayTimeout,
            NokoWebHttpStatusCode.HttpVersionNotSupported => HttpVersionNotSupported,
            NokoWebHttpStatusCode.VariantAlsoNegotiates => VariantAlsoNegotiates,
            NokoWebHttpStatusCode.InsufficientStorage => InsufficientStorage,
            NokoWebHttpStatusCode.LoopDetected => LoopDetected,
            NokoWebHttpStatusCode.NotExtended => NotExtended,
            NokoWebHttpStatusCode.NetworkAuthenticationRequired => NetworkAuthenticationRequired,
            _ => throw new Exception($"Unsupported status code {code}"),
        };
    }
}

public static class HttpStatusCodeExtensions
{
    public static string GetKey(this NokoWebHttpStatusCode code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this NokoWebHttpStatusCode code)
    {
        return NokoWebHttpStatusCodeValues.FromCode(code);
    }
}

public class JsonHttpStatusCodeSerializeConverter : JsonConverter<NokoWebHttpStatusCode>
{
    public override NokoWebHttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NokoWebHttpStatusCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
