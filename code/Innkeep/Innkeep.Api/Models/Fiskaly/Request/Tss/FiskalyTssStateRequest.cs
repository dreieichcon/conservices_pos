using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Tss;

namespace Innkeep.Api.Models.Fiskaly.Request.Tss;

public class FiskalyTssStateRequest
{
	[JsonPropertyName("state")]
	public required TssState State { get; set; }
	
	[JsonPropertyName("description")]
	public string? Description { get; set; }
}