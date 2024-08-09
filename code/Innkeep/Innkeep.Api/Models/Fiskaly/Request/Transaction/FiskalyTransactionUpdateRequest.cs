using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;

namespace Innkeep.Api.Models.Fiskaly.Request.Transaction;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyTransactionUpdateRequest
{
	[JsonIgnore]
	public string TssId { get; set; } = "";
	
	[JsonIgnore]
	public int TransactionRevision { get; set; }

	[JsonIgnore]
	public string TransactionId { get; set; } = "";
	
	[JsonPropertyName("schema")]
	public required FiskalyTransactionSchema Schema { get; set; }

	[JsonPropertyName("state")]
	public required TransactionState State { get; set; } = TransactionState.Active;
	
	[JsonPropertyName("client_id")]
	public required string ClientId { get; set; }
	
}