using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.Checkin;

public class PretixCheckin
{
	[JsonPropertyName("secret")]
	public required string Secret { get; set; }

	[JsonPropertyName("source_type")]
	public string SourceType { get; set; } = "barcode";

	[JsonPropertyName("type")]
	public string Type { get; set; } = "entry";

	[JsonPropertyName("lists")]
	public List<int> CheckinLists { get; set; } = [];

	[JsonPropertyName("force")]
	public bool Force { get; set; }

	[JsonPropertyName("ignore_unpaid")]
	public bool IgnoreUnpaid { get; set; }
}