using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request.Auth;

public class FiskalyAdminPinRequest
{
	[JsonPropertyName("admin_puk")]
	public required string AdminPuk { get; set; }
	
	[JsonPropertyName("new_admin_pin")]
	public required string NewAdminPin { get; set; }
}