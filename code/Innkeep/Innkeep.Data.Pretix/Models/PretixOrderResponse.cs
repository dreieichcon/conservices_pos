using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixOrderResponse
{
	[JsonPropertyName("code")]
	public string Code { get; set; }
	
	[JsonPropertyName("secret")]
	public string Secret { get; set; }

	[JsonPropertyName("nonce")]
	public string Nonce => Guid.NewGuid().ToString();
}