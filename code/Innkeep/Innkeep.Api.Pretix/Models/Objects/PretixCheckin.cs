using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Models.Objects;

public class PretixCheckin
{
	[JsonPropertyName("secret")]
	public required string Secret { get; set; }
	
	[JsonPropertyName("source_type")]
	public required string SourceType { get; set; }
	
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	[JsonPropertyName("lists")]
	public IList<string> Lists => new List<string>()
	{
		"POS"
	};
	
	[JsonPropertyName("force")]
	public bool Force { get; set; }
}