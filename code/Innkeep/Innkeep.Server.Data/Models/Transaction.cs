namespace Innkeep.Server.Data.Models;

public class Transaction
{
	public int Id { get; set; }

	public string TseToken { get; set; }
	
	public string PretixOrderNumber { get; set; }
	
	public Event PretixEvent { get; set; }
	
	public Register Device { get; set; }

	public Guid TransactionId { get; set; }
	
	public DateTime TimeStamp { get; set; }
	
	public string Items { get; set; }
}