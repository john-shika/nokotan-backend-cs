using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Net;

public enum NokoHttpStatusCode
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
    
    public static NokoHttpStatusCode ParseCode(string code)
    {
        return NokoTransformText.ToSnakeCaseUpper(code) switch
        {
            Continue => NokoHttpStatusCode.Continue,
            SwitchingProtocols => NokoHttpStatusCode.SwitchingProtocols,
            Processing => NokoHttpStatusCode.Processing,
            EarlyHints => NokoHttpStatusCode.EarlyHints,
            Ok => NokoHttpStatusCode.Ok,
            Created => NokoHttpStatusCode.Created,
            Accepted => NokoHttpStatusCode.Accepted,
            NonAuthoritativeInformation => NokoHttpStatusCode.NonAuthoritativeInformation,
            NoContent => NokoHttpStatusCode.NoContent,
            ResetContent => NokoHttpStatusCode.ResetContent,
            PartialContent => NokoHttpStatusCode.PartialContent,
            MultiStatus => NokoHttpStatusCode.MultiStatus,
            AlreadyReported => NokoHttpStatusCode.AlreadyReported,
            ImUsed => NokoHttpStatusCode.ImUsed,
            MultipleChoices => NokoHttpStatusCode.MultipleChoices,
            MovedPermanently => NokoHttpStatusCode.MovedPermanently,
            Found => NokoHttpStatusCode.Found,
            SeeOther => NokoHttpStatusCode.SeeOther,
            NotModified => NokoHttpStatusCode.NotModified,
            UseProxy => NokoHttpStatusCode.UseProxy,
            Unused => NokoHttpStatusCode.Unused,
            TemporaryRedirect => NokoHttpStatusCode.TemporaryRedirect,
            PermanentRedirect => NokoHttpStatusCode.PermanentRedirect,
            BadRequest => NokoHttpStatusCode.BadRequest,
            Unauthorized => NokoHttpStatusCode.Unauthorized,
            PaymentRequired => NokoHttpStatusCode.PaymentRequired,
            Forbidden => NokoHttpStatusCode.Forbidden,
            NotFound => NokoHttpStatusCode.NotFound,
            MethodNotAllowed => NokoHttpStatusCode.MethodNotAllowed,
            NotAcceptable => NokoHttpStatusCode.NotAcceptable,
            ProxyAuthenticationRequired => NokoHttpStatusCode.ProxyAuthenticationRequired,
            RequestTimeout => NokoHttpStatusCode.RequestTimeout,
            Conflict => NokoHttpStatusCode.Conflict,
            Gone => NokoHttpStatusCode.Gone,
            LengthRequired => NokoHttpStatusCode.LengthRequired,
            PreconditionFailed => NokoHttpStatusCode.PreconditionFailed,
            PayloadTooLarge => NokoHttpStatusCode.PayloadTooLarge,
            RequestUriTooLong => NokoHttpStatusCode.RequestUriTooLong,
            UnsupportedMediaType => NokoHttpStatusCode.UnsupportedMediaType,
            RequestedRangeNotSatisfiable => NokoHttpStatusCode.RequestedRangeNotSatisfiable,
            ExpectationFailed => NokoHttpStatusCode.ExpectationFailed,
            ImATeapot => NokoHttpStatusCode.ImATeapot,
            InsufficientSpaceOnResource => NokoHttpStatusCode.InsufficientSpaceOnResource,
            MethodFailure => NokoHttpStatusCode.MethodFailure,
            MisdirectedRequest => NokoHttpStatusCode.MisdirectedRequest,
            UnprocessableEntity => NokoHttpStatusCode.UnprocessableEntity,
            Locked => NokoHttpStatusCode.Locked,
            FailedDependency => NokoHttpStatusCode.FailedDependency,
            UpgradeRequired => NokoHttpStatusCode.UpgradeRequired,
            PreconditionRequired => NokoHttpStatusCode.PreconditionRequired,
            TooManyRequests => NokoHttpStatusCode.TooManyRequests,
            RequestHeaderFieldsTooLarge => NokoHttpStatusCode.RequestHeaderFieldsTooLarge,
            UnavailableForLegalReasons => NokoHttpStatusCode.UnavailableForLegalReasons,
            InternalServerError => NokoHttpStatusCode.InternalServerError,
            NotImplemented => NokoHttpStatusCode.NotImplemented,
            BadGateway => NokoHttpStatusCode.BadGateway,
            ServiceUnavailable => NokoHttpStatusCode.ServiceUnavailable,
            GatewayTimeout => NokoHttpStatusCode.GatewayTimeout,
            HttpVersionNotSupported => NokoHttpStatusCode.HttpVersionNotSupported,
            VariantAlsoNegotiates => NokoHttpStatusCode.VariantAlsoNegotiates,
            InsufficientStorage => NokoHttpStatusCode.InsufficientStorage,
            LoopDetected => NokoHttpStatusCode.LoopDetected,
            NotExtended => NokoHttpStatusCode.NotExtended,
            NetworkAuthenticationRequired => NokoHttpStatusCode.NetworkAuthenticationRequired,
            _ => throw new FormatException($"Invalid status code {code}"),
        };
    }

    public static string FromCode(NokoHttpStatusCode code)
    {
        return code switch
        {
            NokoHttpStatusCode.Continue => Continue,
            NokoHttpStatusCode.SwitchingProtocols => SwitchingProtocols,
            NokoHttpStatusCode.Processing => Processing,
            NokoHttpStatusCode.EarlyHints => EarlyHints,
            NokoHttpStatusCode.Ok => Ok,
            NokoHttpStatusCode.Created => Created,
            NokoHttpStatusCode.Accepted => Accepted,
            NokoHttpStatusCode.NonAuthoritativeInformation => NonAuthoritativeInformation,
            NokoHttpStatusCode.NoContent => NoContent,
            NokoHttpStatusCode.ResetContent => ResetContent,
            NokoHttpStatusCode.PartialContent => PartialContent,
            NokoHttpStatusCode.MultiStatus => MultiStatus,
            NokoHttpStatusCode.AlreadyReported => AlreadyReported,
            NokoHttpStatusCode.ImUsed => ImUsed,
            NokoHttpStatusCode.MultipleChoices => MultipleChoices,
            NokoHttpStatusCode.MovedPermanently => MovedPermanently,
            NokoHttpStatusCode.Found => Found,
            NokoHttpStatusCode.SeeOther => SeeOther,
            NokoHttpStatusCode.NotModified => NotModified,
            NokoHttpStatusCode.UseProxy => UseProxy,
            NokoHttpStatusCode.Unused => Unused,
            NokoHttpStatusCode.TemporaryRedirect => TemporaryRedirect,
            NokoHttpStatusCode.PermanentRedirect => PermanentRedirect,
            NokoHttpStatusCode.BadRequest => BadRequest,
            NokoHttpStatusCode.Unauthorized => Unauthorized,
            NokoHttpStatusCode.PaymentRequired => PaymentRequired,
            NokoHttpStatusCode.Forbidden => Forbidden,
            NokoHttpStatusCode.NotFound => NotFound,
            NokoHttpStatusCode.MethodNotAllowed => MethodNotAllowed,
            NokoHttpStatusCode.NotAcceptable => NotAcceptable,
            NokoHttpStatusCode.ProxyAuthenticationRequired => ProxyAuthenticationRequired,
            NokoHttpStatusCode.RequestTimeout => RequestTimeout,
            NokoHttpStatusCode.Conflict => Conflict,
            NokoHttpStatusCode.Gone => Gone,
            NokoHttpStatusCode.LengthRequired => LengthRequired,
            NokoHttpStatusCode.PreconditionFailed => PreconditionFailed,
            NokoHttpStatusCode.PayloadTooLarge => PayloadTooLarge,
            NokoHttpStatusCode.RequestUriTooLong => RequestUriTooLong,
            NokoHttpStatusCode.UnsupportedMediaType => UnsupportedMediaType,
            NokoHttpStatusCode.RequestedRangeNotSatisfiable => RequestedRangeNotSatisfiable,
            NokoHttpStatusCode.ExpectationFailed => ExpectationFailed,
            NokoHttpStatusCode.ImATeapot => ImATeapot,
            NokoHttpStatusCode.InsufficientSpaceOnResource => InsufficientSpaceOnResource,
            NokoHttpStatusCode.MethodFailure => MethodFailure,
            NokoHttpStatusCode.MisdirectedRequest => MisdirectedRequest,
            NokoHttpStatusCode.UnprocessableEntity => UnprocessableEntity,
            NokoHttpStatusCode.Locked => Locked,
            NokoHttpStatusCode.FailedDependency => FailedDependency,
            NokoHttpStatusCode.UpgradeRequired => UpgradeRequired,
            NokoHttpStatusCode.PreconditionRequired => PreconditionRequired,
            NokoHttpStatusCode.TooManyRequests => TooManyRequests,
            NokoHttpStatusCode.RequestHeaderFieldsTooLarge => RequestHeaderFieldsTooLarge,
            NokoHttpStatusCode.UnavailableForLegalReasons => UnavailableForLegalReasons,
            NokoHttpStatusCode.InternalServerError => InternalServerError,
            NokoHttpStatusCode.NotImplemented => NotImplemented,
            NokoHttpStatusCode.BadGateway => BadGateway,
            NokoHttpStatusCode.ServiceUnavailable => ServiceUnavailable,
            NokoHttpStatusCode.GatewayTimeout => GatewayTimeout,
            NokoHttpStatusCode.HttpVersionNotSupported => HttpVersionNotSupported,
            NokoHttpStatusCode.VariantAlsoNegotiates => VariantAlsoNegotiates,
            NokoHttpStatusCode.InsufficientStorage => InsufficientStorage,
            NokoHttpStatusCode.LoopDetected => LoopDetected,
            NokoHttpStatusCode.NotExtended => NotExtended,
            NokoHttpStatusCode.NetworkAuthenticationRequired => NetworkAuthenticationRequired,
            _ => throw new Exception($"Unsupported status code {code}"),
        };
    }
}

public static class HttpStatusCodeExtensions
{
    public static string GetKey(this NokoHttpStatusCode code)
    {
        // return Enum.GetName(code)!;
        return code.ToString();
    }

    public static string GetValue(this NokoHttpStatusCode code)
    {
        return NokoWebHttpStatusCodeValues.FromCode(code);
    }
}

public class JsonHttpStatusCodeSerializeConverter : JsonConverter<NokoHttpStatusCode>
{
    public override NokoHttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NokoHttpStatusCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
