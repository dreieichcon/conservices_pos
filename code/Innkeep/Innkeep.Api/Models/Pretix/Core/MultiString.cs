using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Core;

public class MultiString
{
    [JsonPropertyName("de-informal")]
    public string GermanInformal { get; set; } = string.Empty;

    [JsonPropertyName("de")] 
    public string? GermanDefault { get; set; }

    [JsonPropertyName("en-informal")]
    public string EnglishInformal { get; set; } = string.Empty;
    
    [JsonPropertyName("en")]
    public string? EnglishDefault { get; set; }

    [JsonIgnore]
    public string German => string.IsNullOrEmpty(GermanDefault) ? GermanInformal : GermanDefault;

    [JsonIgnore]
    public string English => string.IsNullOrEmpty(EnglishDefault) ? EnglishInformal : EnglishDefault;
    
    public override string ToString()
    {
        return German;
    }
}