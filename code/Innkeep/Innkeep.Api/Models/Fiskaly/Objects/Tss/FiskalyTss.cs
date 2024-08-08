using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Tss;

namespace Innkeep.Api.Models.Fiskaly.Objects.Tss;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class FiskalyTss
{
	[JsonPropertyName("certificate")]
	public required string Certificate { get; set; }

	[JsonPropertyName("serial_number")]
	public required string SerialNumber { get; set; }

	[JsonPropertyName("public_key")]
	public required string PublicKey { get; set; }

	[JsonPropertyName("signature_algorithm")]
	public required string SignatureAlgorithm { get; set; }

	[JsonPropertyName("signature_timestamp_format")]
	public required string SignatureTimestampFormat { get; set; }

	[JsonPropertyName("transaction_data_encoding")]
	public required string TransactionDataEncoding { get; set; }

	[JsonPropertyName("max_number_registered_clients")]
	public required long MaxRegisteredClients { get; set; }

	[JsonPropertyName("max_number_active_transactions")]
	public required long MaxActiveTransactions { get; set; }

	[JsonPropertyName("supported_update_variants")]
	public required string SupportedUpdateVariants { get; set; }

	[JsonPropertyName("_id")]
	public required string Id { get; set; }

	[JsonPropertyName("_type")]
	public required string Type { get; set; }

	[JsonPropertyName("_env")]
	public required string Environment { get; set; }

	[JsonPropertyName("_version")]
	public required string Version { get; set; }

	[JsonPropertyName("time_creation")]
	public required DateTime TimeCreation { get; set; }

	[JsonPropertyName("description")]
	public required string Description { get; set; }

	[JsonPropertyName("state")]
	public required TssState State { get; set; }

	[JsonPropertyName("bsi_certification_id")]
	public string BsiCertificationId { get; set; } = "";

	[JsonPropertyName("bsi_certification_valid_to")]
	public DateTime BsiCertificationValidTo { get; set; }

	[JsonPropertyName("signature_counter")]
	public long SignatureCounter { get; set; }

	[JsonPropertyName("transaction_counter")]
	public long TransactionCounter { get; set; }

	[JsonPropertyName("number_registered_clients")]
	public long NumberRegisteredClients { get; set; }

	[JsonPropertyName("number_active_transactions")]
	public long NumberActiveTransactions { get; set; }

	[JsonPropertyName("time_uninit")]
	public DateTime TimeUnInit { get; set; }

	[JsonPropertyName("time_init")]
	public DateTime TimeInit { get; set; }

	[JsonPropertyName("time_defective")]
	public DateTime TimeDefective { get; set; }

	[JsonPropertyName("time_disable")]
	public DateTime TimeDisable { get; set; }

	[JsonPropertyName("admin_puk")]
	public string AdminPuk { get; set; } = "";
}