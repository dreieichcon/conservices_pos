using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Response;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyTokenResponse
{
	[JsonPropertyName("access_token")]
	public required string Token { get; set; }
	
	[JsonPropertyName("access_token_expires_at")]
	public DateTime TokenValidUntil { get; set; }
}