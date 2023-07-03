using System.Text;
using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixOrganizer
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("public_url")]
    public string PublicUrl { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Slug: {Slug}");
        sb.AppendLine($"Public Url: {PublicUrl}");
        
        return sb.ToString();
    }
}