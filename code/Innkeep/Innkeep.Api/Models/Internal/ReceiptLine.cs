using System.Globalization;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Internal;

public class ReceiptLine
{
	public string Name { get; set; } = "";
	
	public int Count { get; set; }
	
	public decimal Price { get; set; }
	
	public string TaxRate { get; set; } 
	
	public bool HasTax { get; set; }

	public static ReceiptLine FromCart(DtoSalesItem salesItem)
	{
		return new ReceiptLine()
		{
			Name = salesItem.Name,
			Count = salesItem.CartCount,
			Price = salesItem.PriceWithTax,
			HasTax = salesItem.TaxRate > 0,
			TaxRate = (salesItem.TaxRate / 100).ToString("P0", CultureInfo.InvariantCulture).Replace(" ", ""),
		};
	}

	[JsonIgnore]
	public string CountString => (Count.ToString().PadLeft(2, '0') + "x").PadRight(8);

	[JsonIgnore]
	public string NameString
	{
		get
		{
			var maxLength = 20;
			
			if (!HasTax) 
				maxLength += 4;
			
			var str = Name.Length > maxLength ? Name[..maxLength] : Name;

			return str.PadRight(maxLength);
		}
	}

	[JsonIgnore]
	public string TaxRateString => HasTax ? TaxRate.PadLeft(4) : string.Empty;

	[JsonIgnore]
	public string PriceString => Price.ToString("F2", CultureInfo.InvariantCulture).PadLeft(10);
}