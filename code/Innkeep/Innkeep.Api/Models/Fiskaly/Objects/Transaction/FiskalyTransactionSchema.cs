using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects.Transaction;

// ReSharper disable once ClassNeverInstantiated.Global
public class FiskalyTransactionSchema
{
	[JsonPropertyName("standard_v1")]
	public FiskalySchemaStandardV1? StandardV1 { get; set; }

	[JsonPropertyName("dsfinvtw_v1")]
	public FiskalySchemaDsfintvwV1? DsfinvtwV1 { get; set; }
}