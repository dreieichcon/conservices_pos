using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyAdminAuthenticationRequest
{
	[JsonPropertyName("admin_pin")]
	public string AdminPin { get; set; }
}