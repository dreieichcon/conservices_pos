using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyTransactionLog
{
	[JsonPropertyName("operation")]
	public required string Operation { get; set; }

	[JsonPropertyName("timestamp")]
	public required int Timestamp { get; set; }

	[JsonPropertyName("timestamp_format")]
	public required string TimestampFormat { get; set; }
}