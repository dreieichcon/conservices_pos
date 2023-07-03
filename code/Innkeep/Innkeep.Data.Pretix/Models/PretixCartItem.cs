namespace Innkeep.Data.Pretix.Models;

public class PretixCartItem<T> where T : PretixSalesItem
{
    public PretixCartItem(T item)
    {
        Item = item;
    }

    public T Item { get; set; }
    
    public int Count { get; set; }

    public decimal Price => Item.DefaultPrice * Count;

    private decimal TaxRate => Item.TaxRate / 100;

    public decimal TaxPrice => Item.TaxRate > 0 ? Math.Round((Item.DefaultPrice * TaxRate) / (1 + TaxRate), 2) * Count : 0;
}
