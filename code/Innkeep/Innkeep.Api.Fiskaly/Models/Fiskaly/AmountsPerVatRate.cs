using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class AmountsPerVatRate
{
	[JsonPropertyName("vat_rate")]
	public required VatRate VatRate { get; set; }
	
	[JsonPropertyName("amount")]
	public required decimal Amount { get; set; }
}