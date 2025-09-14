using System.Text.Json.Serialization;

namespace Iris.Types;

public class GiveResult
{
    [JsonPropertyName("result")]
    public int ResultId { get; set; }
}

public class Result
{
    [JsonPropertyName("result")]
    public bool Ok { get; set; }

}