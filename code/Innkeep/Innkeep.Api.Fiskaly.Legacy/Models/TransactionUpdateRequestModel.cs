using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Legacy.Enums;
using Innkeep.Api.Fiskaly.Legacy.Models.Fiskaly;

namespace Innkeep.Api.Fiskaly.Legacy.Models;

public class TransactionUpdateRequestModel
{
	[JsonPropertyName("schema")]
	public required Schema Schema { get; set; }

	[JsonPropertyName("state")]
	public required TransactionState State { get; set; } = TransactionState.ACTIVE;
	
	[JsonPropertyName("client_id")]
	public required string ClientId { get; set; }
}