using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Legacy.Models.Objects;

public class PretixCheckinResponse
{
	[JsonPropertyName("status")]
	public string Status { get; set; }
	
	[JsonPropertyName("position")]
	public SmallPretixOrderPosition Position { get; set; }
	
	[JsonPropertyName("require_attention")]
	public bool RequireAttention { get; set; }
	
	[JsonPropertyName("detail")]
	public string Detail { get; set; }
	
	[JsonPropertyName("reason")]
	public string Reason { get; set; }
	
	[JsonPropertyName("reason_explanation")]
	public string ReasonExplanation { get; set; }
}