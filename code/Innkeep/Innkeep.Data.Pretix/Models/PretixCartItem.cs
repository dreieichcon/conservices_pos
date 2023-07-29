using System.Text;

namespace Innkeep.Data.Pretix.Models;

public class PretixCartItem<T> where T : PretixSalesItem
{
    public PretixCartItem(T item)
    {
        Item = item;
    }

    public T Item { get; set; }
    
    public int Count { get; set; }

    public string LineInfo(string currency)
    {
        var sb = new StringBuilder();

        sb.Append($"{Count.ToString().PadLeft(2, '0')}x");  // 3 characters
        sb.Append(new string(' ', 3));                      // 3 characters = 6

        var name = ShortName();
        sb.Append(name);                                    

        sb.Append(new string(' ', 25 - name.Length));       // 24 characters = 25
        
        sb.Append(new string(' ', 4));                      // 4 characters = 35
        
        sb.Append(Price.ToString().PadLeft(5,' '));                        // 5 characters = 40
        sb.Append(CurrencySymbol(currency));                        // 1 character = 41

        return sb.ToString();
    }

    private string ShortName()
    {
        return Item.Name.German.Length > 25 ? Item.Name.German[..25] : Item.Name.German;
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

    public decimal TaxPrice => Item.TaxRate > 0 ? Math.Round((Item.DefaultPrice * TaxRate) / (1 + TaxRate), 2) * Count : 0;
}
