using System.Text.Json.Serialization;

namespace Innkeep.Data.Pretix.Models;

public class PretixStatus
{
	[JsonPropertyName("status")]
	public string Status { get; set; }
}