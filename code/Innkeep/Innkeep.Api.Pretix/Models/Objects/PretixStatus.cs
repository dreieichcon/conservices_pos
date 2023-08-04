using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Models.Objects;

public class PretixStatus
{
	[JsonPropertyName("status")]
	public required string Status { get; set; }
}