using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyTssStateRequest
{
	[JsonPropertyName("state")]
	public string State { get; set; }
	
	[JsonPropertyName("description")]
	public string Description { get; set; }
}