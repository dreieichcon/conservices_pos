using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Order;

namespace Innkeep.Api.Models.Pretix.Objects.Checkin;

public class PretixCheckinResponse
{
	[JsonPropertyName("status")]
	public CheckinStatus Status { get; set; }

	[JsonPropertyName("reason")]
	public string Reason { get; set; } = "";
	
	public string ReasonFormatted {
		get
		{
			if (Reason == "already_redeemed")
				return "Already Scanned";

			return "";
		}
	}

	[JsonPropertyName("require_attention")]
	public bool RequireAttention { get; set; }

	[JsonPropertyName("checkin_texts")]
	public IList<object> CheckinTexts { get; set; } = [];
	
	[JsonPropertyName("position")]
	public PretixOrderPosition? RelatedOrder { get; set; }
	
	[JsonIgnore]
	public DateTime Scanned { get; init; } = DateTime.Now; 
}