using System.Globalization;
using System.Text;
using Innkeep.Api.Pretix.Legacy.Models.Objects;

namespace Innkeep.Api.Pretix.Legacy.Models.Internal;

public class PretixCartItem<T> where T : PretixSalesItem
{
	private const int LineLength = 37;
	
	public PretixCartItem(T item)
	{
		Item = item;
	}

	public T Item { get; set; }

	public int Count { get; set; }
	
	public string LineInfo(string currency)
	{
		var sb = new StringBuilder();

		sb.Append($"{Count.ToString().PadLeft(2, '0')}x".PadRight(5, ' ')); // 5 characters
		sb.Append(ShortName());

		return sb.ToString();
	}

	public string LinePricing(string currency)
	{
		var sb = new StringBuilder();

		sb.Append(new string(' ', 5)); // 5  characters                        
		sb.Append($"je {Item.DefaultPriceString.PadLeft(5, ' ')}"); // 5 characters = 10
		sb.Append(CurrencySymbol(currency)); // 1 character = 11

		var total = Price.ToString(CultureInfo.InvariantCulture).PadLeft(5, ' ') +
					CurrencySymbol(currency); // 6 characters

		sb.Append(total.PadLeft(26, ' '));

		return sb.ToString();
	}

	private string ShortName()
	{
		return Item.Name.German.Length > LineLength ? Item.Name.German[..LineLength] : Item.Name.German;
	}

	public decimal Price => Item.DefaultPrice * Count;

	private decimal TaxRate => Item.TaxRate / 100;

	private string CurrencySymbol(string currency)
	{
		return currency.ToLower() switch
		{
			"eur" => "€",
			_ => "$"
		};
	}

	public decimal TaxPrice =>
		Item.TaxRate > 0 ? Math.Round((Item.DefaultPrice * TaxRate) / (1 + TaxRate), 2) * Count : 0;
}