using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixCheckin
{
	[JsonPropertyName("secret")]
	public string Secret { get; set; }
	
	[JsonPropertyName("source_type")]
	public string SourceType { get; set; }
	
	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("lists")]
	public IList<string> Lists => new List<string>()
	{
		"POS"
	};
	
	[JsonPropertyName("force")]
	public bool Force { get; set; }
}