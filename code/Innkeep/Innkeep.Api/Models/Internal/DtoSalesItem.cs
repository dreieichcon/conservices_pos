using System.Text.Json.Serialization;
using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Api.Models.Internal;

public class DtoSalesItem
{
	public int Id { get; set; }
	
	#region Pricing
	public required string Name { get; set; }
	
	public required decimal Price { get; set; }
	
	public required decimal TaxRate { get; set; }
	
	public required string Currency { get; set; }

	[JsonIgnore]
	public decimal TaxAmount => Price * (TaxRate / 100);
	
	[JsonIgnore]
	public decimal PriceWithTax => Price * (1 + TaxRate / 100);

	[JsonIgnore]
	public decimal TotalPrice => PriceWithTax * CartCount;

	[JsonIgnore]
	public decimal TotalTax => TaxAmount * CartCount;
	
	#endregion
	
	#region Quota
	public int CartCount { get; set; }
	
	public int? QuotaMax { get; set; }
	
	public int? QuotaLeft { get; set; }

	[JsonIgnore]
	public int? QuotaSold => QuotaMax - QuotaLeft;
	
	public bool SoldOut { get; set; }

	[JsonIgnore]
	public bool Infinite => QuotaLeft == null;
	
	#endregion
	
	public bool PrintCheckinVoucher { get; set; }

	public static DtoSalesItem FromPretix(PretixSalesItem pretixSalesItem)
	{
		return new DtoSalesItem()
		{
			Id = pretixSalesItem.Id,
			Name = pretixSalesItem.Name.German,
			Price = pretixSalesItem.DefaultPrice,
			TaxRate = pretixSalesItem.TaxRate,
			Currency = pretixSalesItem.Currency,
			PrintCheckinVoucher = !pretixSalesItem.Admission,
		};
	}
}