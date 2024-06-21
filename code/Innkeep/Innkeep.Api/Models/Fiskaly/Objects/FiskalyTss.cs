using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Fiskaly.Objects;

public class FiskalyTss
{
	[JsonPropertyName("certificate")]
	public string Certificate { get; set; }

	[JsonPropertyName("serial_number")]
	public string SerialNumber { get; set; }

	[JsonPropertyName("public_key")]
	public string PublicKey { get; set; }

	[JsonPropertyName("signature_algorithm")]
	public string SignatureAlgorithm { get; set; }

	[JsonPropertyName("signature_timestamp_format")]
	public string SignatureTimestampFormat { get; set; }

	[JsonPropertyName("transaction_data_encoding")]
	public string TransactionDataEncoding { get; set; }

	[JsonPropertyName("max_number_registered_clients")]
	public long Max_Number_Registered_Clients { get; set; }

	[JsonPropertyName("max_number_active_transactions")]
	public long Max_Number_Active_Transactions { get; set; }

	[JsonPropertyName("supported_update_variants")]
	public string Supported_Update_Variants { get; set; }

	[JsonPropertyName("_id")]
	public string Id { get; set; }

	[JsonPropertyName("_type")]
	public string Type { get; set; }

	[JsonPropertyName("_env")]
	public string Environment { get; set; }

	[JsonPropertyName("_version")]
	public string Version { get; set; }

	[JsonPropertyName("time_creation")]
	public DateTime TimeCreation { get; set; }

	[JsonPropertyName("description")]
	public string Description { get; set; }

	[JsonPropertyName("state")]
	public string State { get; set; }

	[JsonPropertyName("bsi_certification_id")]
	public string BsiCertificationId { get; set; }

	[JsonPropertyName("bsi_certification_valid_to")]
	public int BsiCertificationValidTo { get; set; }

	[JsonPropertyName("signature_counter")]
	public string SignatureCounter { get; set; }

	[JsonPropertyName("transaction_counter")]
	public string TransactionCounter { get; set; }

	[JsonPropertyName("number_registered_clients")]
	public long NumberRegisteredClients { get; set; }

	[JsonPropertyName("number_active_transactions")]
	public long NumberActiveTransactions { get; set; }

	[JsonPropertyName("time_uninit")]
	public DateTime TimeUninit { get; set; }

	[JsonPropertyName("time_init")]
	public DateTime TimeInit { get; set; }

	[JsonPropertyName("time_defective")]
	public DateTime TimeDefective { get; set; }

	[JsonPropertyName("time_disable")]
	public DateTime TimeDisable { get; set; }
}