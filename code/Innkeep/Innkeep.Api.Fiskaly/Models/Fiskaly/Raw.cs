using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class Raw
{
	[JsonPropertyName("process_type")]
	public string ProcessType { get; set; }
	
	[JsonPropertyName("process_data")]
	public string ProcessData { get; set; }
}