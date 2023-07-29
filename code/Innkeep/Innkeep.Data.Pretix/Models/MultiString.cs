using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class MultiString
{
    [JsonPropertyName("de-informal")]
    public string GermanInformal { get; set; }
    
    [JsonPropertyName("de")]
    public string GermanDefault { get; set; }

    [JsonIgnore]
    public string German => string.IsNullOrEmpty(GermanDefault) ? GermanInformal : GermanDefault;
    
    public override string ToString()
    {
        return German;
    }
}