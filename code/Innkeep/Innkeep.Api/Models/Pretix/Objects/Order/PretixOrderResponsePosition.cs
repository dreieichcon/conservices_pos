using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.Order;

public class PretixOrderResponsePosition : PretixOrderPosition
{
	[JsonPropertyName("secret")]
	public string Secret { get; set; } = "";
}