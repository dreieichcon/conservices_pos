using System.Globalization;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class LineItem
{
	[JsonPropertyName("quantity")]
	public string Quantity { get; set; }

	[JsonIgnore]
	public decimal QuantityDecimal
	{
		get => decimal.TryParse(Quantity, out var amount) ? amount : 0;
		set => Quantity = value.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
	}
	
	[JsonPropertyName("text")]
	public string Text { get; set; }
	
	[JsonPropertyName("price_per_unit")]
	public string PricePerUnit { get; set; }

	[JsonIgnore]
	public decimal PricePerUnitDecimal
	{
		get => decimal.TryParse(PricePerUnit, out var amount) ? amount : 0;
		set => PricePerUnit = value.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
	}
		
	
}