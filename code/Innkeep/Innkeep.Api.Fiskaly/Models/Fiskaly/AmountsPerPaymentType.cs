using System.Globalization;
using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class AmountsPerPaymentType
{
	[JsonPropertyName("payment_type")]
	public PaymentType PaymentType { get; set; }
	
	[JsonPropertyName("amount")]
	public string Amount { get; set; }
    
	[JsonIgnore]
	public decimal DecimalAmount
	{
		get => decimal.TryParse(Amount, out var amount) ? amount : 0;
		set => Amount = value.ToString("0.00").Replace(",",".");
	}

	[JsonPropertyName("currency_code")]
	public CurrencyCode CurrencyCode { get; set; }
}