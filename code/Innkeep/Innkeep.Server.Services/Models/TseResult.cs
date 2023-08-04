namespace Innkeep.Server.Services.Models;

public class TseResult
{
	public DateTime StartTime { get; set; }
	
	public DateTime EndTime { get; set; }
	
	public required string TseTransactionNumber { get; set; }
	
	public required string Checksum { get; set; }
	
	public required string Signature { get; set; }
}