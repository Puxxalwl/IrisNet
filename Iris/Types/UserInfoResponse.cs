using System.Text.Json.Serialization;

namespace Iris.Types;


public class SpamResponse
{
    [JsonPropertyName("scam")]
    public bool IsScam { get; set; }

    [JsonPropertyName("spam")]
    public bool IsSpam { get; set; }

    [JsonPropertyName("ignore")]
    public bool IsIgnore { get; set; }
}
public class ResultSpam
{
    [JsonPropertyName("result")]
    public SpamResponse Result { get; set; } = null!;
}

public class ResultReg
{
    [JsonPropertyName("result")]
    public long Result { get; set; }
}

public class ResultStars
{
    [JsonPropertyName("result")]
    public long Starts { get; set; }
}

public class ResultBag
{
    [JsonPropertyName("result")]
    public BagResponse Result { get; set; } = null!;
}
public class BagResponse
{
    [JsonPropertyName("sweets")]
    public double Sweets { get; set; }

    [JsonPropertyName("gold")]
    public int IrisGold { get; set; }

    [JsonPropertyName("stars")]
    public int Stars { get; set; }

    [JsonPropertyName("coins")]
    public int Coins { get; set; }
}

public class ResultActivity
{
    [JsonPropertyName("result")]
    public ActivityResponse Result { get; set; } = null!;
}
public class ActivityResponse
{
    [JsonPropertyName("day")]
    public double Day { get; set; }

    [JsonPropertyName("week")]
    public int Week { get; set; }

    [JsonPropertyName("month")]
    public int Month { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}