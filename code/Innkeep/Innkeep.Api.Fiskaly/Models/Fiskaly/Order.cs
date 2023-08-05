using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class Order
{
	[JsonPropertyName("line_items")]
	public IList<LineItem> LineItems { get; set; }
}