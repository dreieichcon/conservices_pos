using System.Text.Json;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models;

public class AuthenticateApiRequestModel
{
	[JsonPropertyName("api_key")]
	public string ApiKey { get; set; }
	
	[JsonPropertyName("api_secret")]
	public string ApiSecret { get; set; }

	public string ToJson()
	{
		return JsonSerializer.Serialize(this);
	}
}