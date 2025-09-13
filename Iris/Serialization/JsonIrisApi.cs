using System.Text.Json;
using System.Text.Json.Serialization;
namespace Iris.Serialization;

public static class JsonIrisApi
{
    public static JsonSerializerOptions Options { get; }
    static JsonIrisApi() => Configure(Options = new());

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

#if NET6_0_OR_GREATER
        if (!JsonSerializer.IsReflectionEnabledByDefault)
        {
            options.TypeInfoResolverChain.Add(JsonIrisSerializerContext.Default);
        }
        #endif
    }
}