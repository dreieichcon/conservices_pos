using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyAmountPerVatRate
{
	[JsonPropertyName("vat_rate")]
	public VatRate VatRate { get; set; }

	[JsonPropertyName("amount")]
	public required decimal Amount { get; set; }
}