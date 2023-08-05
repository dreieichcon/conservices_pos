using System.Text.Json.Serialization;
using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Models.Fiskaly;

namespace Innkeep.Api.Fiskaly.Models;

public class TransactionResponseModel
{
    [JsonPropertyName("number")]
    public long Number { get; set; }
    
    [JsonPropertyName("time_start")]
    public int TimeStart { get; set; }
    
    [JsonPropertyName("client_serial_number")]
    public required string ClientSerialNumber { get; set; }
    
    [JsonPropertyName("tss_serial_number")]
    public required string TssSerialNumber { get; set; }
    
    [JsonPropertyName("state")]
    public TransactionState State { get; set; }
    
    [JsonPropertyName("client_id")]
    public required string ClientId { get; set; }
    
    [JsonPropertyName("schema")]
    public required Schema Schema { get; set; }
    
    [JsonPropertyName("revision")]
    public required int Revision { get; set; }
    
    [JsonPropertyName("latest_revision")]
    public required int LatestRevision { get; set; }
    
    [JsonPropertyName("tss_id")]
    public required string TssId { get; set; }
    
    // [JsonPropertyName("metadata")]
    // public required Metadata Metadata { get; set; }
    
    [JsonPropertyName("_type")]
    public required string Type { get; set; }
    
    [JsonPropertyName("_id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("_env")]
    public required string Env { get; set; }
    
    [JsonPropertyName("_version")]
    public required string Version { get; set; }
    
    [JsonPropertyName("time_end")]
    public required int TimeEnd { get; set; }
    
    [JsonPropertyName("qr_code_data")]
    public required string QrCodeData { get; set; }
    
    [JsonPropertyName("log")]
    public required Log Log { get; set; }
    
    [JsonPropertyName("signature")]
    public required Signature Signature { get; set; }

    [JsonIgnore]
    public DateTime StartTime => DateTimeOffset.FromUnixTimeSeconds(TimeStart).DateTime;

    [JsonIgnore]
    public DateTime EndTime => DateTimeOffset.FromUnixTimeSeconds(TimeEnd).DateTime;
}
