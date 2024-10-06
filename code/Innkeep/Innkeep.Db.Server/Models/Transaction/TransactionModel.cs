using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demolite.Db.Models;

namespace Innkeep.Db.Server.Models.Transaction;

public class TransactionModel : AbstractDbItem
{
	public DateTime TransactionDate { get; set; }

	[MaxLength(32)]
	public string ReceiptType { get; set; } = "";

	[MaxLength(40)]
	public string TssId { get; set; } = "";

	[MaxLength(40)]
	public string ClientId { get; set; } = "";

	[MaxLength(40)]
	public string EventId { get; set; } = "";

	[MaxLength(40)]
	public string OrderSecret { get; set; } = "";

	[MaxLength(40)]
	public string OrderCode { get; set; } = "";

	[MaxLength(40)]
	public string RegisterId { get; set; } = "";

	public decimal AmountRequested { get; set; }

	public decimal AmountGiven { get; set; }

	public decimal AmountBack { get; set; }

	public string ReceiptJson { get; set; } = "";

	[NotMapped]
	public decimal TotalChange => Math.Abs(AmountGiven) - Math.Abs(AmountBack);
}