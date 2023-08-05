using System.Text.Json.Serialization;

namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class Schema
{
	[JsonPropertyName("standard_v1")]
	public StandardV1 StandardV1 { get; set; }
    
	[JsonPropertyName("raw")]
	public Raw? Raw { get; set; }
}