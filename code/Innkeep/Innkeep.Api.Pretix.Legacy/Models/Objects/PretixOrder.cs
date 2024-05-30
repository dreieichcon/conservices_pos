using System.Text.Json.Serialization;

namespace Innkeep.Api.Pretix.Legacy.Models.Objects;

public class PretixOrder
{
	[JsonPropertyName("status")]
	public required string Status { get; set; }
	
	[JsonPropertyName("email")]
	public required string Mail { get; set; }
	
	[JsonPropertyName("locale")]
	public required string Locale { get; set; }
	
	[JsonPropertyName("sales_channel")]
	public required string SalesChannel { get; set; }

	[JsonPropertyName("fees")] 
	[JsonIgnore]
	public IList<string> Fees { get; set; } = new List<string>();

	[JsonPropertyName("payment_provider")] 
	public string PaymentProvider { get; set; } = "manual";

	[JsonPropertyName("simulate")]
	public bool Simulate { get; set; }
	
	[JsonPropertyName("testmode")]
	public bool TestMode { get; set; }

	[JsonPropertyName("customer")]
	[JsonIgnore]
	public PretixCustomer? Customer { get; set; }
	
	[JsonPropertyName("invoice_address")]
	[JsonIgnore]
	public PretixInvoiceAddress? InvoiceAddress { get; set; }

	[JsonPropertyName("positions")]
	public IList<PretixOrderPosition> Positions { get; set; } = new List<PretixOrderPosition>();
}