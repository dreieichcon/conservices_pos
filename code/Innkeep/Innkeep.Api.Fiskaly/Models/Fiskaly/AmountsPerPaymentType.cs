using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class AmountsPerPaymentType
{
	[JsonPropertyName("payment_type")]
	public PaymentType PaymentType { get; set; }
	
	[JsonPropertyName("amount")]
	public decimal Amount { get; set; }
	
	[JsonPropertyName("currency_code")]
	public CurrencyCode CurrencyCode { get; set; }
}