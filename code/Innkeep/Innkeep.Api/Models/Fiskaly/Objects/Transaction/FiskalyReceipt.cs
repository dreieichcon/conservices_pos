using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyReceipt
{
	[JsonPropertyName("receipt_type")]
	public required ReceiptType ReceiptType { get; set; }

	[JsonPropertyName("amounts_per_vat_rate")]
	public IList<FiskalyAmountPerVatRate> AmountsPerVatRate { get; set; } = [];
	
	[JsonPropertyName("amounts_per_payment_type")]
	public IList<FiskalyAmountPerPaymentType> AmountsPerPaymentType { get; set; } = [];
}