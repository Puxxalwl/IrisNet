using System.Text.Json.Serialization;

namespace Iris.Types;

public class Balance
{
    // Ириски
    [JsonPropertyName("sweets")]
    public double Sweets { get; set; }

    // Ирис-голд
    [JsonPropertyName("gold")]
    public double IrisGold { get; set; }

    // Очки доната
    [JsonPropertyName("donate_score")]
    public int DonateScore { get; set; }

}