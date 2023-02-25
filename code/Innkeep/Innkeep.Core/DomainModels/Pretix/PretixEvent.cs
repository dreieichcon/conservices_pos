using System.Text;
using System.Text.Json.Serialization;
using Innkeep.Core.Utilities;

namespace Innkeep.Core.DomainModels.Pretix;

public class PretixEvent
{
    [JsonPropertyName("name")]
    public MultiString Name { get; set; }
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("live")]
    public bool IsLive { get; set; }
    
    [JsonPropertyName("testmode")]
    public bool IsTestMode { get; set; }
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    
    [JsonPropertyName("date_from")]
    public DateTime? EventStart { get; set; }
    
    [JsonPropertyName("date_to")]
    public DateTime? EventEnd { get; set; }
    
    [JsonPropertyName("is_public")]
    public bool IsPublic { get; set; }
    
    [JsonPropertyName("presale_start")]
    public DateTime? PresaleStart { get; set; }
    
    [JsonPropertyName("presale_end")]
    public DateTime? PresaleEnd { get; set; }
    
    [JsonPropertyName("location")]
    public MultiString? Location { get; set; }
    
    [JsonPropertyName("sales_channels")]
    public IEnumerable<string> SalesChannels { get; set; }

    public override string ToString()
    {
        return ClassDebugger.CreateDebugString(this);
    }
}