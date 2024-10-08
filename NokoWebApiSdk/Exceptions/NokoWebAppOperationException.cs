namespace NokoWebApiSdk.Exceptions;

public class NokoWebAppOperationException(string methodName)
    : InvalidOperationException($"Method NokoWebApplication.{methodName} must be called before calling this method.");