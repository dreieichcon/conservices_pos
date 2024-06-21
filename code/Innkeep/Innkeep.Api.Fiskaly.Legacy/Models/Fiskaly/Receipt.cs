using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Legacy.Models.Fiskaly;

public class Receipt
{
	[JsonPropertyName("receipt_type")]
	public string ReceiptType { get; set; }
	
	[JsonPropertyName("amounts_per_vat_rate")]
	public required IList<AmountsPerVatRate> AmountsPerVatRate { get; set; }
	
	[JsonPropertyName("amounts_per_payment_type")]
	public required IList<AmountsPerPaymentType> AmountsPerPaymentType { get; set; }
}