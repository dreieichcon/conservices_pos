using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Models.Objects;

public class SmallPretixOrderPosition
{
	[JsonPropertyName("positionid")]
	public int PositionId { get; set; }
	
	[JsonPropertyName("item")]
	public int Item { get; set; }
	
	[JsonPropertyName("variation")]
	public int? Variation { get; set; }
	
	[JsonPropertyName("attendee_name")]
	public required string AttendeeName { get; set; }
}