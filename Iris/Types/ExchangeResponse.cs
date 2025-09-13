using System.Text.Json.Serialization;
using System.Collections.Generic;

public class TradesDetalis
{
    [JsonPropertyName("volume")]
    public int Volume { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }
}

public class Trades
{
    [JsonPropertyName("buy")]
    public List<TradesDetalis> Buy { get; set; } = null!;

    [JsonPropertyName("sell")]
    public List<TradesDetalis> Sell { get; set; } = null!;
}

public class TradesDeals
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    [JsonPropertyName("date")]
    public double Date { get; set; }

    [JsonPropertyName("volume")]
    public int Volume { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}