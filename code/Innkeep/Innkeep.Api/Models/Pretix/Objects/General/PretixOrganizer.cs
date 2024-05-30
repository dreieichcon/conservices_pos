using System.Text;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.General;

public class PretixOrganizer
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("slug")]
    public required string Slug { get; set; }
    
    [JsonPropertyName("public_url")]
    public required string PublicUrl { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Slug: {Slug}");
        sb.AppendLine($"Public Url: {PublicUrl}");
        
        return sb.ToString();
    }
}