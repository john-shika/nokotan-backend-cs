using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using NokoWebApiSdk.Schemas;

namespace NokoWebApiSdk.Json.Serializers;

[JsonConverter(typeof(MessageBody<>))]
public partial class MessageBodySerializer(JsonSerializerOptions options) : JsonSerializerContext(options)
{
    public override JsonTypeInfo? GetTypeInfo(Type type)
    {
        return null;
    }

    protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = new();
}
