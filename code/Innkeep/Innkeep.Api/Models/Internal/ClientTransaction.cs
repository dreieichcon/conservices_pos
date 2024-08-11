namespace Innkeep.Api.Models.Internal;

public class ClientTransaction
{
	public IEnumerable<DtoSalesItem> SalesItems { get; set; } = [];
	
	public decimal AmountGiven { get; set; }
}