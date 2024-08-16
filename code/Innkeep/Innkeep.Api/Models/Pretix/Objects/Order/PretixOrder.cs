using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.Order;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class PretixOrder
{
	[JsonPropertyName("status")]
	public string Status { get; set; } = "p";

	[JsonPropertyName("email")]
	public string Mail { get; set; } = "";

	[JsonPropertyName("locale")]
	public required string Locale { get; set; }

	[JsonPropertyName("sales_channel")]
	public string SalesChannel { get; set; } = "pretixpos";

	[JsonPropertyName("payment_provider")]
	public string PaymentProvider { get; set; } = "manual";

	[JsonPropertyName("simulate")]
	public bool Simulate { get; set; }

	[JsonPropertyName("testmode")]
	public bool TestMode { get; set; }

	[JsonPropertyName("positions")]
	public IList<PretixOrderPosition> Positions { get; set; } = new List<PretixOrderPosition>();
}