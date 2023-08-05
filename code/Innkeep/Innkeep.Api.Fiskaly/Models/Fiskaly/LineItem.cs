using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class LineItem
{
	[JsonPropertyName("quantity")]
	public decimal Quantity { get; set; }
	
	[JsonPropertyName("text")]
	public string Text { get; set; }
	
	[JsonPropertyName("price_per_unit")]
	public decimal PricePerUnit { get; set; }
}