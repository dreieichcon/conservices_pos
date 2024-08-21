using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.Sales;

public class PretixQuota
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = "";
	
	[JsonPropertyName("size")]
	public int? Size { get; set; }

	[JsonPropertyName("items")]
	public List<int> Items { get; set; } = [];

	[JsonPropertyName("variations")]
	public List<int> Variations { get; set; } = [];
	
	[JsonPropertyName("closed")]
	public bool Closed { get; set; }
	
	[JsonPropertyName("available_number")]
	public int? AmountAvailable { get; set; }

	[JsonIgnore]
	public bool Unlimited => Size == null;
}