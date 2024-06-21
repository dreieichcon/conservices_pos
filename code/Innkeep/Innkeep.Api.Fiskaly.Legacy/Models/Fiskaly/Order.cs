using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Legacy.Models.Fiskaly;

public class Order
{
	[JsonPropertyName("line_items")]
	public IList<LineItem> LineItems { get; set; }
}