using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Response;

public class FiskalyTokenResponse
{
	[JsonPropertyName("access_token")]
	public string Token { get; set; }
	
	[JsonPropertyName("access_token_expires_at")]
	public DateTime TokenValidUntil { get; set; }
}