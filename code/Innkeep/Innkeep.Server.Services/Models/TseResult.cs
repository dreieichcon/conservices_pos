namespace Innkeep.Server.Services.Models;

public class TseResult
{
	public DateTime StartTime { get; set; }
	
	public DateTime EndTime { get; set; }
	
	public string TseTransactionNumber { get; set; }
	
	public string Checksum { get; set; }
	
	public string Signature { get; set; }
}