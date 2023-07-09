namespace Innkeep.Server.Data.Models;

public class Transaction
{
	public int Id { get; set; }

	public string TseToken { get; set; } = null!;
	
	public string PretixOrderNumber { get; set; } = null!;
	public Organizer Organizer { get; set; } = null!;
	public Event Event { get; set; } = null!;
	public Register Device { get; set; } = null!;

	public Guid TransactionId { get; set; }
	
	public DateTime TimeStamp { get; set; }
	
	public string Items { get; set; } = null!;
}