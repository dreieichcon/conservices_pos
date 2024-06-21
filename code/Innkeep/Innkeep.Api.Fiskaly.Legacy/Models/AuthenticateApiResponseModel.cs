using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Legacy.Models;

public class AuthenticateApiResponseModel
{
	[JsonPropertyName("access_token")]
	public required string AccessToken { get; set; }

	[JsonPropertyName("access_token_expires_in")]
	public int AccessTokenExpiresIn { get; set; }
	
	public DateTime AccessTokenExpiresAtDateTime
	=> DateTimeOffset.FromUnixTimeSeconds(AccessTokenExpiresAt).DateTime;

	[JsonPropertyName("access_token_expires_at")]
	public int AccessTokenExpiresAt { get; set; }
}