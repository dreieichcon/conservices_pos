using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Legacy.Models.Objects;

public class PretixOrderResponse
{
	[JsonPropertyName("code")]
	public required string Code { get; set; }
	
	[JsonPropertyName("secret")]
	public required string Secret { get; set; }

	[JsonPropertyName("nonce")]
	public string Nonce => Guid.NewGuid().ToString();
}