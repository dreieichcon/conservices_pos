using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

public class FiskalyTransactionSignature
{
	[JsonPropertyName("value")]
	public required string Value { get; set; }

	[JsonPropertyName("algorithm")]
	public required string Algorithm { get; set; }

	[JsonPropertyName("counter")]
	public required string Counter { get; set; }

	[JsonPropertyName("public_key")]
	public required string PublicKey { get; set; }
}