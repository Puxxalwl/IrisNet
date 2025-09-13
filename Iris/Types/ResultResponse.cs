using System.Text.Json.Serialization;

namespace Iris.Types;

public class GiveResult
{
    [JsonPropertyName("result")]
    public int? ResultId { get; set; }

    // Возможная ошибка
    [JsonPropertyName("error")]
    public ErrorResponse Error = null!;
}

public class Result
{
    [JsonPropertyName("result")]
    public bool? Ok { get; set; } = null;

    // Возможная ошибка
    [JsonPropertyName("error")]
    public ErrorResponse Error = null!;

}