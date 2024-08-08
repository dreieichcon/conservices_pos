using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;

namespace Innkeep.Api.Models.Fiskaly.Request.Transaction;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyTransactionStartRequest
{
	[JsonPropertyName("state")]
	public required TransactionState State { get; set; }
	
	[JsonPropertyName("client_id")]
	public required Guid ClientId { get; set; }
}