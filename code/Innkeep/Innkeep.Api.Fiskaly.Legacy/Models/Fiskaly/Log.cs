using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Legacy.Models.Fiskaly;

public class Log
{
	[JsonPropertyName("operation")]
	public required string Operation { get; set; }
	
	[JsonPropertyName("timestamp")]
	public int Timestamp { get; set; }
	
	[JsonPropertyName("timestamp_format")]
	public required string TimestampFormat { get; set; }
}