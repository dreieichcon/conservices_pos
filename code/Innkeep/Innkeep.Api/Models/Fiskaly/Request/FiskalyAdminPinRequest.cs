using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Request;

public class FiskalyAdminPinRequest
{
	[JsonPropertyName("admin_puk")]
	public string AdminPuk { get; set; }
	
	[JsonPropertyName("new_admin_pin")]
	public string NewAdminPin { get; set; }
}