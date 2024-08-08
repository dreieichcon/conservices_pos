using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Client;

namespace Innkeep.Api.Models.Fiskaly.Request.Client;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyClientUpdateRequest
{
	[JsonPropertyName("state")]
	public ClientState State { get; set; }
}