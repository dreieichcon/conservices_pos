using System.Text.Json.Serialization;
using Innkeep.Api.Pretix.Legacy.Models.Base;
using Innkeep.Core.Utilities;

namespace Innkeep.Api.Pretix.Legacy.Models.Objects;

public class PretixEvent
{
    [JsonPropertyName("name")]
    public required MultiString Name { get; set; }
    
    [JsonPropertyName("slug")]
    public required string Slug { get; set; }
    
    [JsonPropertyName("live")]
    public required bool IsLive { get; set; }
    
    [JsonPropertyName("testmode")]
    public required bool IsTestMode { get; set; }
    
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }
    
    [JsonPropertyName("date_from")]
    public DateTime? EventStart { get; set; }
    
    [JsonPropertyName("date_to")]
    public DateTime? EventEnd { get; set; }
    
    [JsonPropertyName("is_public")]
    public required bool IsPublic { get; set; }
    
    [JsonPropertyName("presale_start")]
    public DateTime? PresaleStart { get; set; }
    
    [JsonPropertyName("presale_end")]
    public DateTime? PresaleEnd { get; set; }
    
    [JsonPropertyName("location")]
    public MultiString? Location { get; set; }
    
    [JsonPropertyName("sales_channels")]
    public required IEnumerable<string> SalesChannels { get; set; }

    public override string ToString()
    {
        return ClassDebugger.CreateDebugString(this);
    }
}