using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Cores.Net;

public enum NokoHttpStatusCodes
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

public static class NokoHttpStatusCodeValues
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
    
    public static NokoHttpStatusCodes ParseCode(string code)
    {
        return NokoTransformText.ToSnakeCaseUpper(code) switch
        {
            Continue => NokoHttpStatusCodes.Continue,
            SwitchingProtocols => NokoHttpStatusCodes.SwitchingProtocols,
            Processing => NokoHttpStatusCodes.Processing,
            EarlyHints => NokoHttpStatusCodes.EarlyHints,
            Ok => NokoHttpStatusCodes.Ok,
            Created => NokoHttpStatusCodes.Created,
            Accepted => NokoHttpStatusCodes.Accepted,
            NonAuthoritativeInformation => NokoHttpStatusCodes.NonAuthoritativeInformation,
            NoContent => NokoHttpStatusCodes.NoContent,
            ResetContent => NokoHttpStatusCodes.ResetContent,
            PartialContent => NokoHttpStatusCodes.PartialContent,
            MultiStatus => NokoHttpStatusCodes.MultiStatus,
            AlreadyReported => NokoHttpStatusCodes.AlreadyReported,
            ImUsed => NokoHttpStatusCodes.ImUsed,
            MultipleChoices => NokoHttpStatusCodes.MultipleChoices,
            MovedPermanently => NokoHttpStatusCodes.MovedPermanently,
            Found => NokoHttpStatusCodes.Found,
            SeeOther => NokoHttpStatusCodes.SeeOther,
            NotModified => NokoHttpStatusCodes.NotModified,
            UseProxy => NokoHttpStatusCodes.UseProxy,
            Unused => NokoHttpStatusCodes.Unused,
            TemporaryRedirect => NokoHttpStatusCodes.TemporaryRedirect,
            PermanentRedirect => NokoHttpStatusCodes.PermanentRedirect,
            BadRequest => NokoHttpStatusCodes.BadRequest,
            Unauthorized => NokoHttpStatusCodes.Unauthorized,
            PaymentRequired => NokoHttpStatusCodes.PaymentRequired,
            Forbidden => NokoHttpStatusCodes.Forbidden,
            NotFound => NokoHttpStatusCodes.NotFound,
            MethodNotAllowed => NokoHttpStatusCodes.MethodNotAllowed,
            NotAcceptable => NokoHttpStatusCodes.NotAcceptable,
            ProxyAuthenticationRequired => NokoHttpStatusCodes.ProxyAuthenticationRequired,
            RequestTimeout => NokoHttpStatusCodes.RequestTimeout,
            Conflict => NokoHttpStatusCodes.Conflict,
            Gone => NokoHttpStatusCodes.Gone,
            LengthRequired => NokoHttpStatusCodes.LengthRequired,
            PreconditionFailed => NokoHttpStatusCodes.PreconditionFailed,
            PayloadTooLarge => NokoHttpStatusCodes.PayloadTooLarge,
            RequestUriTooLong => NokoHttpStatusCodes.RequestUriTooLong,
            UnsupportedMediaType => NokoHttpStatusCodes.UnsupportedMediaType,
            RequestedRangeNotSatisfiable => NokoHttpStatusCodes.RequestedRangeNotSatisfiable,
            ExpectationFailed => NokoHttpStatusCodes.ExpectationFailed,
            ImATeapot => NokoHttpStatusCodes.ImATeapot,
            InsufficientSpaceOnResource => NokoHttpStatusCodes.InsufficientSpaceOnResource,
            MethodFailure => NokoHttpStatusCodes.MethodFailure,
            MisdirectedRequest => NokoHttpStatusCodes.MisdirectedRequest,
            UnprocessableEntity => NokoHttpStatusCodes.UnprocessableEntity,
            Locked => NokoHttpStatusCodes.Locked,
            FailedDependency => NokoHttpStatusCodes.FailedDependency,
            UpgradeRequired => NokoHttpStatusCodes.UpgradeRequired,
            PreconditionRequired => NokoHttpStatusCodes.PreconditionRequired,
            TooManyRequests => NokoHttpStatusCodes.TooManyRequests,
            RequestHeaderFieldsTooLarge => NokoHttpStatusCodes.RequestHeaderFieldsTooLarge,
            UnavailableForLegalReasons => NokoHttpStatusCodes.UnavailableForLegalReasons,
            InternalServerError => NokoHttpStatusCodes.InternalServerError,
            NotImplemented => NokoHttpStatusCodes.NotImplemented,
            BadGateway => NokoHttpStatusCodes.BadGateway,
            ServiceUnavailable => NokoHttpStatusCodes.ServiceUnavailable,
            GatewayTimeout => NokoHttpStatusCodes.GatewayTimeout,
            HttpVersionNotSupported => NokoHttpStatusCodes.HttpVersionNotSupported,
            VariantAlsoNegotiates => NokoHttpStatusCodes.VariantAlsoNegotiates,
            InsufficientStorage => NokoHttpStatusCodes.InsufficientStorage,
            LoopDetected => NokoHttpStatusCodes.LoopDetected,
            NotExtended => NokoHttpStatusCodes.NotExtended,
            NetworkAuthenticationRequired => NokoHttpStatusCodes.NetworkAuthenticationRequired,
            _ => throw new FormatException($"Invalid status code {code}"),
        };
    }

    public static string FromCode(NokoHttpStatusCodes codes)
    {
        return codes switch
        {
            NokoHttpStatusCodes.Continue => Continue,
            NokoHttpStatusCodes.SwitchingProtocols => SwitchingProtocols,
            NokoHttpStatusCodes.Processing => Processing,
            NokoHttpStatusCodes.EarlyHints => EarlyHints,
            NokoHttpStatusCodes.Ok => Ok,
            NokoHttpStatusCodes.Created => Created,
            NokoHttpStatusCodes.Accepted => Accepted,
            NokoHttpStatusCodes.NonAuthoritativeInformation => NonAuthoritativeInformation,
            NokoHttpStatusCodes.NoContent => NoContent,
            NokoHttpStatusCodes.ResetContent => ResetContent,
            NokoHttpStatusCodes.PartialContent => PartialContent,
            NokoHttpStatusCodes.MultiStatus => MultiStatus,
            NokoHttpStatusCodes.AlreadyReported => AlreadyReported,
            NokoHttpStatusCodes.ImUsed => ImUsed,
            NokoHttpStatusCodes.MultipleChoices => MultipleChoices,
            NokoHttpStatusCodes.MovedPermanently => MovedPermanently,
            NokoHttpStatusCodes.Found => Found,
            NokoHttpStatusCodes.SeeOther => SeeOther,
            NokoHttpStatusCodes.NotModified => NotModified,
            NokoHttpStatusCodes.UseProxy => UseProxy,
            NokoHttpStatusCodes.Unused => Unused,
            NokoHttpStatusCodes.TemporaryRedirect => TemporaryRedirect,
            NokoHttpStatusCodes.PermanentRedirect => PermanentRedirect,
            NokoHttpStatusCodes.BadRequest => BadRequest,
            NokoHttpStatusCodes.Unauthorized => Unauthorized,
            NokoHttpStatusCodes.PaymentRequired => PaymentRequired,
            NokoHttpStatusCodes.Forbidden => Forbidden,
            NokoHttpStatusCodes.NotFound => NotFound,
            NokoHttpStatusCodes.MethodNotAllowed => MethodNotAllowed,
            NokoHttpStatusCodes.NotAcceptable => NotAcceptable,
            NokoHttpStatusCodes.ProxyAuthenticationRequired => ProxyAuthenticationRequired,
            NokoHttpStatusCodes.RequestTimeout => RequestTimeout,
            NokoHttpStatusCodes.Conflict => Conflict,
            NokoHttpStatusCodes.Gone => Gone,
            NokoHttpStatusCodes.LengthRequired => LengthRequired,
            NokoHttpStatusCodes.PreconditionFailed => PreconditionFailed,
            NokoHttpStatusCodes.PayloadTooLarge => PayloadTooLarge,
            NokoHttpStatusCodes.RequestUriTooLong => RequestUriTooLong,
            NokoHttpStatusCodes.UnsupportedMediaType => UnsupportedMediaType,
            NokoHttpStatusCodes.RequestedRangeNotSatisfiable => RequestedRangeNotSatisfiable,
            NokoHttpStatusCodes.ExpectationFailed => ExpectationFailed,
            NokoHttpStatusCodes.ImATeapot => ImATeapot,
            NokoHttpStatusCodes.InsufficientSpaceOnResource => InsufficientSpaceOnResource,
            NokoHttpStatusCodes.MethodFailure => MethodFailure,
            NokoHttpStatusCodes.MisdirectedRequest => MisdirectedRequest,
            NokoHttpStatusCodes.UnprocessableEntity => UnprocessableEntity,
            NokoHttpStatusCodes.Locked => Locked,
            NokoHttpStatusCodes.FailedDependency => FailedDependency,
            NokoHttpStatusCodes.UpgradeRequired => UpgradeRequired,
            NokoHttpStatusCodes.PreconditionRequired => PreconditionRequired,
            NokoHttpStatusCodes.TooManyRequests => TooManyRequests,
            NokoHttpStatusCodes.RequestHeaderFieldsTooLarge => RequestHeaderFieldsTooLarge,
            NokoHttpStatusCodes.UnavailableForLegalReasons => UnavailableForLegalReasons,
            NokoHttpStatusCodes.InternalServerError => InternalServerError,
            NokoHttpStatusCodes.NotImplemented => NotImplemented,
            NokoHttpStatusCodes.BadGateway => BadGateway,
            NokoHttpStatusCodes.ServiceUnavailable => ServiceUnavailable,
            NokoHttpStatusCodes.GatewayTimeout => GatewayTimeout,
            NokoHttpStatusCodes.HttpVersionNotSupported => HttpVersionNotSupported,
            NokoHttpStatusCodes.VariantAlsoNegotiates => VariantAlsoNegotiates,
            NokoHttpStatusCodes.InsufficientStorage => InsufficientStorage,
            NokoHttpStatusCodes.LoopDetected => LoopDetected,
            NokoHttpStatusCodes.NotExtended => NotExtended,
            NokoHttpStatusCodes.NetworkAuthenticationRequired => NetworkAuthenticationRequired,
            _ => throw new Exception($"Unsupported status code {codes}"),
        };
    }
}

public static class NokoHttpStatusCodeExtensions
{
    public static string GetKey(this NokoHttpStatusCodes codes)
    {
        // return Enum.GetName(code)!;
        return codes.ToString();
    }

    public static string GetValue(this NokoHttpStatusCodes codes)
    {
        return NokoHttpStatusCodeValues.FromCode(codes);
    }
}

public class NokoJsonHttpStatusCodeSerializerConverter : JsonConverter<NokoHttpStatusCodes>
{
    public override NokoHttpStatusCodes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NokoHttpStatusCodes value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
