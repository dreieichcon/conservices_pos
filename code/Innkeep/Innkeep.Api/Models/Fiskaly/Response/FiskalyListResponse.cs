using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Response;

public class FiskalyListResponse<T>
{
	[JsonPropertyName("data")]
	public IEnumerable<T> Data { get; set; }
}