using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixResponse<T>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("next")]
    public object? Next { get; set; }
    
    [JsonPropertyName("previous")]
    public object? Previous { get; set; }
    
    [JsonPropertyName("results")]
    public IEnumerable<T> Results { get; set; }
}