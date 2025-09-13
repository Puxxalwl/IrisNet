using System.Dynamic;
using System.Text.Json.Serialization;

namespace Iris.Types;

public class ApiError
{
    [JsonPropertyName("error")]
    public ErrorResponse Error { get; set; } = null!;
}

public class ErrorResponse
{
    // Описание ошибки
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    // Код ошибки
    [JsonPropertyName("code")]
    public int Code { get; set; }
}