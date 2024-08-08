using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Client;

namespace Innkeep.Api.Models.Fiskaly.Request.Client;

public class FiskalyClientUpdateRequest
{
	[JsonPropertyName("state")]
	public ClientState State { get; set; }
}