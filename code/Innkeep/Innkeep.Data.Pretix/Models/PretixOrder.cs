using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixOrder
{
	[JsonPropertyName("status")]
	public string Status { get; set; }
	
	[JsonPropertyName("email")]
	public string Mail { get; set; }
	
	[JsonPropertyName("locale")]
	public string Locale { get; set; }
	
	[JsonPropertyName("sales_channel")]
	public string SalesChannel { get; set; }

	[JsonPropertyName("fees")] 
	[JsonIgnore]
	public IList<string> Fees { get; set; } = new List<string>();

	[JsonPropertyName("payment_provider")] 
	public string PaymentProvider { get; set; } = "manual";

	[JsonPropertyName("simulate")]
	public bool Simulate { get; set; }
	
	[JsonPropertyName("customer")]
	[JsonIgnore]
	public PretixCustomer? Customer { get; set; }
	
	[JsonPropertyName("invoice_address")]
	[JsonIgnore]
	public PretixInvoiceAddress? InvoiceAddress { get; set; }
	
	[JsonPropertyName("positions")]
	public IList<PretixOrderPosition> Positions { get; set; }
}