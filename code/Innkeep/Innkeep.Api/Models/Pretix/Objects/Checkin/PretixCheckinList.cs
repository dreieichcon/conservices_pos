using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.Checkin;

public class PretixCheckinList
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	[JsonPropertyName("checkin_count")]
	public int CheckinCount { get; set; }

	[JsonPropertyName("limit_products")]
	public List<int> LimitProducts { get; set; } = [];

	[JsonPropertyName("position_count")]
	public int PositionCount { get; set; }

	[JsonPropertyName("all_products")]
	public bool AllProducts { get; set; }

	[JsonPropertyName("include_pending")]
	public bool IncludePending { get; set; }

	[JsonPropertyName("allow_multiple_entries")]
	public bool AllowMultipleEntries { get; set; }

	[JsonPropertyName("allow_entry_after_exit")]
	public bool AllowEntryAfterExit { get; set; }

	[JsonPropertyName("addon_match")]
	public bool AddonMatch { get; set; }
}