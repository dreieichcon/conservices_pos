using System.Globalization;
using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class AmountsPerVatRate
{
	[JsonPropertyName("vat_rate")]
	public required VatRate VatRate { get; set; }
	
	[JsonPropertyName("amount")]
	public string Amount { get; set; }
    
	[JsonIgnore]
	public decimal DecimalAmount
	{
		get => decimal.TryParse(Amount, out var amount) ? amount : 0;
		set => Amount = value.ToString("0.00").Replace(",",".");
	}
}