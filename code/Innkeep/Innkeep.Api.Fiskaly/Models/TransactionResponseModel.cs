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
    public string ClientSerialNumber { get; set; }
    
    [JsonPropertyName("tss_serial_number")]
    public string TssSerialNumber { get; set; }
    
    [JsonPropertyName("state")]
    public TransactionState State { get; set; }
    
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }
    
    [JsonPropertyName("schema")]
    public Schema Schema { get; set; }
    
    [JsonPropertyName("revision")]
    public int Revision { get; set; }
    
    [JsonPropertyName("latest_revision")]
    public int LatestRevision { get; set; }
    
    [JsonPropertyName("tss_id")]
    public string TssId { get; set; }
    
    // [JsonPropertyName("metadata")]
    // public required Metadata Metadata { get; set; }
    
    [JsonPropertyName("_type")]
    public string Type { get; set; }
    
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    
    [JsonPropertyName("_env")]
    public string Env { get; set; }
    
    [JsonPropertyName("_version")]
    public string Version { get; set; }
    
    [JsonPropertyName("time_end")]
    public int TimeEnd { get; set; }
    
    [JsonPropertyName("qr_code_data")]
    public string QrCodeData { get; set; }
    
    [JsonPropertyName("log")]
    public Log Log { get; set; }
    
    [JsonPropertyName("signature")]
    public Signature Signature { get; set; }

    [JsonIgnore]
    public DateTime StartTime => TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeSeconds(TimeStart), TimeZoneInfo.Local).DateTime;

    [JsonIgnore]
    public DateTime EndTime =>
        TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeSeconds(TimeEnd), TimeZoneInfo.Local).DateTime;
}
