using System.ComponentModel.DataAnnotations.Schema;

namespace Innkeep.Server.Data.Models;

public class CashFlow
{
	public int Id { get; set; }
	
	public DateTime TimeStamp { get; set; }
	
	public Event Event { get; set; }
	
	public Register Register { get; set; }
	public decimal MoneyAdded { get; set; }
	
	public decimal MoneyRemoved { get; set; }

	[NotMapped] public decimal TotalMoney => MoneyAdded - MoneyRemoved;
}