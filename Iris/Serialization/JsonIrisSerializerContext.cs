using System.Text.Json.Serialization;
using Iris.Types;

namespace Iris.Serialization;

[JsonSourceGenerationOptions
(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
)
]
[JsonSerializable(typeof(int[]))]
[JsonSerializable(typeof(Balance))]
[JsonSerializable(typeof(UpdateLog[]))]
[JsonSerializable(typeof(Trades))]
[JsonSerializable(typeof(TradesDeals[]))]
[JsonSerializable(typeof(ApiError))]
[JsonSerializable(typeof(GiveResult))]
[JsonSerializable(typeof(Result))]
[JsonSerializable(typeof(Transaction[]))]
public partial class JsonIrisSerializerContext : JsonSerializerContext;