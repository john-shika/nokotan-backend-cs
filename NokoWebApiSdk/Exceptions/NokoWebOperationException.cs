namespace NokoWebApiSdk.Exceptions;

public class NokoWebOperationException(string className, string methodName)
    : InvalidOperationException($"Method {className}.{methodName} must be called before calling this method.");