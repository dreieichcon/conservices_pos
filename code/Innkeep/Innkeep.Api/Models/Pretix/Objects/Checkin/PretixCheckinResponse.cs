using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Pretix;

namespace Innkeep.Api.Models.Pretix.Objects.Checkin;

public class PretixCheckinResponse
{
	[JsonPropertyName("status")]
	public CheckinStatus Status { get; set; }

	[JsonPropertyName("reason")]
	public string Reason { get; set; } = "";

	[JsonPropertyName("require_attention")]
	public bool RequireAttention { get; set; }

	[JsonPropertyName("checkin_texts")]
	public IList<object> CheckinTexts { get; set; } = [];
}