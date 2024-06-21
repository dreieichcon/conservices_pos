using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyTokenRequest
{
	[JsonPropertyName("api_key")]
	public string Key { get; set; }
	
	[JsonPropertyName("api_secret")]
	public string Secret { get; set; }
}