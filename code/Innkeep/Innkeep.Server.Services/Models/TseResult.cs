namespace Innkeep.Server.Services.Models;

public class TseResult
{
	
	public required string TseTransactionNumber { get; set; }
	
	public DateTime StartTime { get; set; }
	
	public DateTime EndTime { get; set; }
    
	public required string TseSerialNumber { get; set; }
	public required string Signature { get; set; }
	
	public required string TseTimestampFormat { get; set; }
	
	public DateTime FirstOrder { get; set; }
	public required string HashAlgorithm { get; set; }
	
	public required string QrCode { get; set; }
	
	public required int SignatureCount { get; set; }
	
	public required string PublicKey { get; set; }
    
}