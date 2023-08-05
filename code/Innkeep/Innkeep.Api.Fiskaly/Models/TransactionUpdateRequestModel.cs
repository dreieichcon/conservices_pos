using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Models.Fiskaly;

namespace Innkeep.Api.Fiskaly.Models;

public class TransactionUpdateRequestModel
{
	[JsonPropertyName("schema")]
	public required Schema Schema { get; set; }

	[JsonPropertyName("state")]
	public required TransactionState State { get; set; } = TransactionState.ACTIVE;
	
	[JsonPropertyName("client_id")]
	public required string ClientId { get; set; }
}