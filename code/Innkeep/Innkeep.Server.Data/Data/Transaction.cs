namespace Innkeep.Server.Data.Data;

public class Transaction
{
	public int Id { get; set; }
	
	public string DeviceId { get; set; }
	
	public string TseToken { get; set; }

	public Guid TransactionId { get; set; }
	
	public DateTime TimeStamp { get; set; }
	
	public string Items { get; set; }
}