using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Response;

// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyListResponse<T>
{
	[JsonPropertyName("data")]
	public IEnumerable<T> Data { get; set; } = [];
}