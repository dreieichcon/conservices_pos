using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Models.Base;

public class MultiString
{
    [JsonPropertyName("de-informal")]
    public string GermanInformal { get; set; } = "Empty";

    [JsonPropertyName("de")] public string? GermanDefault { get; set; } = "Empty";

    [JsonIgnore]
    public string German => string.IsNullOrEmpty(GermanDefault) ? GermanInformal : GermanDefault;
    
    public override string? ToString()
    {
        return German;
    }
}