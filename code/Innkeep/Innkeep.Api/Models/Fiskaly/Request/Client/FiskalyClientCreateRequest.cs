using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request.Client;

public class FiskalyClientCreateRequest
{
	[JsonPropertyName("serial_number")]
	public required string SerialNumber { get; set; }
}