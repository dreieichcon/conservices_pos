using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Enum.Shared;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyAmountPerPaymentType
{
	[JsonPropertyName("payment_type")]
	public PaymentType PaymentType { get; set; }

	[JsonPropertyName("amount")]
	public decimal Amount { get; set; }

	[JsonPropertyName("currency_code")]
	public CurrencyCode CurrencyCode { get; set; }
}