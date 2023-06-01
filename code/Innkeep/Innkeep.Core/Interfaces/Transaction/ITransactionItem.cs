namespace Innkeep.Core.Interfaces.Transaction;

public interface ITransactionItem
{
	public decimal Price { get; set; }
	
	public decimal TaxPrice { get; set; }
	
	public decimal TaxRate { get; set; }
	
	public int Count { get; set; }
	
	public string Currency { get; set; }
}