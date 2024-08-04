using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyClientUpdateRequest
{
	[JsonPropertyName("state")]
	public string State { get; set; }
}