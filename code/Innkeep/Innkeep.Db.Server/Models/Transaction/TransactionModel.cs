using System.ComponentModel.DataAnnotations.Schema;
using Innkeep.Db.Models;

namespace Innkeep.Db.Server.Models.Transaction;

public class TransactionModel : AbstractDbItem
{
	public DateTime TransactionDate { get; set; }

	public string ReceiptType { get; set; } = "";
	
	public string TssId { get; set; } = "";
	
	public string ClientId { get; set; } = "";
	
	public string EventId { get; set; } = "";

	public string OrderSecret { get; set; } = "";
	public decimal AmountRequested { get; set; }
	public decimal AmountGiven { get; set; }
	
	public decimal AmountBack { get; set; }

	public string ReceiptJson { get; set; } = "";

	[NotMapped]
	public decimal TotalChange => AmountGiven - AmountRequested;
}