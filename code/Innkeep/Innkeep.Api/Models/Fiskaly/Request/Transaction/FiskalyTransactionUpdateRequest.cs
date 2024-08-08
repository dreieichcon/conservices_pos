using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;

namespace Innkeep.Api.Models.Fiskaly.Request.Transaction;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyTransactionUpdateRequest
{
	[JsonPropertyName("schema")]
	public required FiskalySchemaStandardV1 Schema { get; set; }

	[JsonPropertyName("state")]
	public required TransactionState State { get; set; } = TransactionState.Active;
	
	[JsonPropertyName("client_id")]
	public required Guid ClientId { get; set; }
}