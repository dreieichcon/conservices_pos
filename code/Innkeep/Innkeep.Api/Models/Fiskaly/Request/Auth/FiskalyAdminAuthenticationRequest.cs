using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request.Auth;

public class FiskalyAdminAuthenticationRequest
{
	[JsonPropertyName("admin_pin")]
	public required string AdminPin { get; set; }
}