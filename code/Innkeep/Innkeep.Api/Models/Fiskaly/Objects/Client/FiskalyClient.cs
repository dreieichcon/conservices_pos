using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Client;

namespace Innkeep.Api.Models.Fiskaly.Objects.Client;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyClient
{
	[JsonPropertyName("_id")]
	public required string Id { get; set; }
	
	[JsonPropertyName("serial_number")]
	public required string SerialNumber { get; set; }
	
	[JsonPropertyName("tss_id")]
	public required string TssId { get; set; }
	
	[JsonPropertyName("state")]
	public required ClientState State { get; set; }
}