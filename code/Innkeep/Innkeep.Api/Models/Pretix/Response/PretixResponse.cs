using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Response;

public class PretixResponse<T>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("next")]
    public object? Next { get; set; }
    
    [JsonPropertyName("previous")]
    public object? Previous { get; set; }
    
    [JsonPropertyName("results")]
    public required IEnumerable<T> Results { get; set; }
}