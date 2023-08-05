using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;

namespace Innkeep.Api.Fiskaly.Models;

public class TransactionStartRequestModel
{
	[JsonPropertyName("state")]
	public required TransactionState State { get; set; }
	
	[JsonPropertyName("client_id")]
	public required Guid ClientId { get; set; }
}