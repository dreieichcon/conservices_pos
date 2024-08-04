using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects;

public class FiskalyClient
{
	[JsonPropertyName("_id")]
	public string Id { get; set; }
	
	[JsonPropertyName("serial_number")]
	public string SerialNumber { get; set; }
	
	[JsonPropertyName("tss_id")]
	public string TssId { get; set; }
	
	[JsonPropertyName("state")]
	public string State { get; set; }
}