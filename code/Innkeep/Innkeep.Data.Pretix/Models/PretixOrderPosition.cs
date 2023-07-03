using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixOrderPosition
{
	[JsonPropertyName("positionid")]
	public int PositionId { get; set; }
	
	[JsonPropertyName("item")]
	public int Item { get; set; }
	
	[JsonPropertyName("price")]
	public decimal Price { get; set; }
	
	[JsonPropertyName("variation")]
	public int? Variation { get; set; }
	
	[JsonPropertyName("attendee_name")]
	public string AttendeeName { get; set; }
}