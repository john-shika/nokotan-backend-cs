using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace NokoWebApiSdk.Transformers;

public static class OperationTransformers
{
    public static OpenApiOptions AddHeader(this OpenApiOptions options, string headerName, string defaultValue)
    {
        return options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var schema = typeof(string).MapTypeToOpenApiPrimitiveType();
            schema.Default = new OpenApiString(defaultValue);
            operation.Parameters ??= [];
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = headerName,
                Schema = schema,
                In = ParameterLocation.Header,
            });
            return Task.CompletedTask;
        });
    }
}