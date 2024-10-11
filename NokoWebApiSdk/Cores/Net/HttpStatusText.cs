using System.Text.RegularExpressions;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Utils;

namespace NokoWebApiSdk.Utils.Net;

public static class HttpStatusText
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
    
    public static HttpStatusCodes ParseCode(string statusText)
    {
        return NokoWebTransformText.ToSnakeCaseUpper(statusText) switch
        {
            Continue => HttpStatusCodes.Continue,
            SwitchingProtocols => HttpStatusCodes.SwitchingProtocols,
            Processing => HttpStatusCodes.Processing,
            EarlyHints => HttpStatusCodes.EarlyHints,
            Ok => HttpStatusCodes.Ok,
            Created => HttpStatusCodes.Created,
            Accepted => HttpStatusCodes.Accepted,
            NonAuthoritativeInformation => HttpStatusCodes.NonAuthoritativeInformation,
            NoContent => HttpStatusCodes.NoContent,
            ResetContent => HttpStatusCodes.ResetContent,
            PartialContent => HttpStatusCodes.PartialContent,
            MultiStatus => HttpStatusCodes.MultiStatus,
            AlreadyReported => HttpStatusCodes.AlreadyReported,
            ImUsed => HttpStatusCodes.ImUsed,
            MultipleChoices => HttpStatusCodes.MultipleChoices,
            MovedPermanently => HttpStatusCodes.MovedPermanently,
            Found => HttpStatusCodes.Found,
            SeeOther => HttpStatusCodes.SeeOther,
            NotModified => HttpStatusCodes.NotModified,
            UseProxy => HttpStatusCodes.UseProxy,
            Unused => HttpStatusCodes.Unused,
            TemporaryRedirect => HttpStatusCodes.TemporaryRedirect,
            PermanentRedirect => HttpStatusCodes.PermanentRedirect,
            BadRequest => HttpStatusCodes.BadRequest,
            Unauthorized => HttpStatusCodes.Unauthorized,
            PaymentRequired => HttpStatusCodes.PaymentRequired,
            Forbidden => HttpStatusCodes.Forbidden,
            NotFound => HttpStatusCodes.NotFound,
            MethodNotAllowed => HttpStatusCodes.MethodNotAllowed,
            NotAcceptable => HttpStatusCodes.NotAcceptable,
            ProxyAuthenticationRequired => HttpStatusCodes.ProxyAuthenticationRequired,
            RequestTimeout => HttpStatusCodes.RequestTimeout,
            Conflict => HttpStatusCodes.Conflict,
            Gone => HttpStatusCodes.Gone,
            LengthRequired => HttpStatusCodes.LengthRequired,
            PreconditionFailed => HttpStatusCodes.PreconditionFailed,
            PayloadTooLarge => HttpStatusCodes.PayloadTooLarge,
            RequestUriTooLong => HttpStatusCodes.RequestUriTooLong,
            UnsupportedMediaType => HttpStatusCodes.UnsupportedMediaType,
            RequestedRangeNotSatisfiable => HttpStatusCodes.RequestedRangeNotSatisfiable,
            ExpectationFailed => HttpStatusCodes.ExpectationFailed,
            ImATeapot => HttpStatusCodes.ImATeapot,
            InsufficientSpaceOnResource => HttpStatusCodes.InsufficientSpaceOnResource,
            MethodFailure => HttpStatusCodes.MethodFailure,
            MisdirectedRequest => HttpStatusCodes.MisdirectedRequest,
            UnprocessableEntity => HttpStatusCodes.UnprocessableEntity,
            Locked => HttpStatusCodes.Locked,
            FailedDependency => HttpStatusCodes.FailedDependency,
            UpgradeRequired => HttpStatusCodes.UpgradeRequired,
            PreconditionRequired => HttpStatusCodes.PreconditionRequired,
            TooManyRequests => HttpStatusCodes.TooManyRequests,
            RequestHeaderFieldsTooLarge => HttpStatusCodes.RequestHeaderFieldsTooLarge,
            UnavailableForLegalReasons => HttpStatusCodes.UnavailableForLegalReasons,
            InternalServerError => HttpStatusCodes.InternalServerError,
            NotImplemented => HttpStatusCodes.NotImplemented,
            BadGateway => HttpStatusCodes.BadGateway,
            ServiceUnavailable => HttpStatusCodes.ServiceUnavailable,
            GatewayTimeout => HttpStatusCodes.GatewayTimeout,
            HttpVersionNotSupported => HttpStatusCodes.HttpVersionNotSupported,
            VariantAlsoNegotiates => HttpStatusCodes.VariantAlsoNegotiates,
            InsufficientStorage => HttpStatusCodes.InsufficientStorage,
            LoopDetected => HttpStatusCodes.LoopDetected,
            NotExtended => HttpStatusCodes.NotExtended,
            NetworkAuthenticationRequired => HttpStatusCodes.NetworkAuthenticationRequired,
            _ => throw new FormatException(statusText),
        };
    }

    public static string FromCode(HttpStatusCodes statusCode)
    {
        return statusCode switch
        {
            HttpStatusCodes.Continue => Continue,
            HttpStatusCodes.SwitchingProtocols => SwitchingProtocols,
            HttpStatusCodes.Processing => Processing,
            HttpStatusCodes.EarlyHints => EarlyHints,
            HttpStatusCodes.Ok => Ok,
            HttpStatusCodes.Created => Created,
            HttpStatusCodes.Accepted => Accepted,
            HttpStatusCodes.NonAuthoritativeInformation => NonAuthoritativeInformation,
            HttpStatusCodes.NoContent => NoContent,
            HttpStatusCodes.ResetContent => ResetContent,
            HttpStatusCodes.PartialContent => PartialContent,
            HttpStatusCodes.MultiStatus => MultiStatus,
            HttpStatusCodes.AlreadyReported => AlreadyReported,
            HttpStatusCodes.ImUsed => ImUsed,
            HttpStatusCodes.MultipleChoices => MultipleChoices,
            HttpStatusCodes.MovedPermanently => MovedPermanently,
            HttpStatusCodes.Found => Found,
            HttpStatusCodes.SeeOther => SeeOther,
            HttpStatusCodes.NotModified => NotModified,
            HttpStatusCodes.UseProxy => UseProxy,
            HttpStatusCodes.Unused => Unused,
            HttpStatusCodes.TemporaryRedirect => TemporaryRedirect,
            HttpStatusCodes.PermanentRedirect => PermanentRedirect,
            HttpStatusCodes.BadRequest => BadRequest,
            HttpStatusCodes.Unauthorized => Unauthorized,
            HttpStatusCodes.PaymentRequired => PaymentRequired,
            HttpStatusCodes.Forbidden => Forbidden,
            HttpStatusCodes.NotFound => NotFound,
            HttpStatusCodes.MethodNotAllowed => MethodNotAllowed,
            HttpStatusCodes.NotAcceptable => NotAcceptable,
            HttpStatusCodes.ProxyAuthenticationRequired => ProxyAuthenticationRequired,
            HttpStatusCodes.RequestTimeout => RequestTimeout,
            HttpStatusCodes.Conflict => Conflict,
            HttpStatusCodes.Gone => Gone,
            HttpStatusCodes.LengthRequired => LengthRequired,
            HttpStatusCodes.PreconditionFailed => PreconditionFailed,
            HttpStatusCodes.PayloadTooLarge => PayloadTooLarge,
            HttpStatusCodes.RequestUriTooLong => RequestUriTooLong,
            HttpStatusCodes.UnsupportedMediaType => UnsupportedMediaType,
            HttpStatusCodes.RequestedRangeNotSatisfiable => RequestedRangeNotSatisfiable,
            HttpStatusCodes.ExpectationFailed => ExpectationFailed,
            HttpStatusCodes.ImATeapot => ImATeapot,
            HttpStatusCodes.InsufficientSpaceOnResource => InsufficientSpaceOnResource,
            HttpStatusCodes.MethodFailure => MethodFailure,
            HttpStatusCodes.MisdirectedRequest => MisdirectedRequest,
            HttpStatusCodes.UnprocessableEntity => UnprocessableEntity,
            HttpStatusCodes.Locked => Locked,
            HttpStatusCodes.FailedDependency => FailedDependency,
            HttpStatusCodes.UpgradeRequired => UpgradeRequired,
            HttpStatusCodes.PreconditionRequired => PreconditionRequired,
            HttpStatusCodes.TooManyRequests => TooManyRequests,
            HttpStatusCodes.RequestHeaderFieldsTooLarge => RequestHeaderFieldsTooLarge,
            HttpStatusCodes.UnavailableForLegalReasons => UnavailableForLegalReasons,
            HttpStatusCodes.InternalServerError => InternalServerError,
            HttpStatusCodes.NotImplemented => NotImplemented,
            HttpStatusCodes.BadGateway => BadGateway,
            HttpStatusCodes.ServiceUnavailable => ServiceUnavailable,
            HttpStatusCodes.GatewayTimeout => GatewayTimeout,
            HttpStatusCodes.HttpVersionNotSupported => HttpVersionNotSupported,
            HttpStatusCodes.VariantAlsoNegotiates => VariantAlsoNegotiates,
            HttpStatusCodes.InsufficientStorage => InsufficientStorage,
            HttpStatusCodes.LoopDetected => LoopDetected,
            HttpStatusCodes.NotExtended => NotExtended,
            HttpStatusCodes.NetworkAuthenticationRequired => NetworkAuthenticationRequired,
            _ => throw new Exception("Invalid status code"),
        };
    }
}