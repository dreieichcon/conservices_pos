using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyClientCreateRequest
{
	[JsonPropertyName("serial_number")]
	public string SerialNumber { get; set; }
}