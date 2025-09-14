using System.Text.Json.Serialization;

namespace Iris.Types;

public class UpdateLog
{
    // Ид события
    [JsonPropertyName("id")]
    public long Id { get; set; }

    // Тип события: sweets_log — логи ирисок, gold_log — логи ирис-голд
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    // Время события
    [JsonPropertyName("date")]
    public long Date { get; set; }

    // Объект собитыя
    [JsonPropertyName("object")]
    public Transaction Obj { get; set; } = null!;


}