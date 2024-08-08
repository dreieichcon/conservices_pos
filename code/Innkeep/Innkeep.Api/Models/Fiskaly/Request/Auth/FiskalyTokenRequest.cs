using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request.Auth;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyTokenRequest
{
	[JsonPropertyName("api_key")]
	public required string Key { get; set; }
	
	[JsonPropertyName("api_secret")]
	public required string Secret { get; set; }
}