using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Api.Models.Internal;

public class DtoSalesItem
{
	public int Id { get; set; }
	
	public string Name { get; set; }
	
	public decimal Price { get; set; }
	
	public decimal TaxRate { get; set; }
	
	public string Currency { get; set; }
	
	public int CartCount { get; set; }

	public static DtoSalesItem FromPretix(PretixSalesItem pretixSalesItem)
	{
		return new DtoSalesItem()
		{
			Id = pretixSalesItem.Id,
			Name = pretixSalesItem.Name.German,
			Price = pretixSalesItem.DefaultPrice,
			TaxRate = pretixSalesItem.TaxRate,
			Currency = pretixSalesItem.Currency,
		};
	}
}