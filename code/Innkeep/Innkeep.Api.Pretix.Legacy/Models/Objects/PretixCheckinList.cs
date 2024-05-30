using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Legacy.Models.Objects;

public class PretixCheckinList
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("checkin_count")]
	public int checkin_count { get; set; }

	[JsonPropertyName("position_count")]
	public int position_count { get; set; }

	[JsonPropertyName("all_products")]
	public bool all_products { get; set; }

	[JsonPropertyName("limit_products")]
	public IList<object> limit_products { get; set; }

	[JsonPropertyName("include_pending")]
	public bool include_pending { get; set; }

	[JsonPropertyName("subevent")]
	public object subevent { get; set; }

	[JsonPropertyName("allow_multiple_entries")]
	public bool allow_multiple_entries { get; set; }

	[JsonPropertyName("allow_entry_after_exit")]
	public bool allow_entry_after_exit { get; set; }

	[JsonPropertyName("exit_all_at")]
	public object exit_all_at { get; set; }

	[JsonPropertyName("addon_match")]
	public bool addon_match { get; set; }
}