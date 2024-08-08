using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalySchemaStandardV1
{
	[JsonPropertyName("receipt")]
	public FiskalyReceipt? Receipt { get; set; }
}