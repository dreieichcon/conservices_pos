using System.Collections.ObjectModel;
using Innkeep.Core.DomainModels.Pretix;
using Innkeep.Core.Interfaces.Services;

namespace Innkeep.DI.Services;

public class ShoppingCartService : IShoppingCartService
{
    public int Amount { get; set; } = 1;

    public ObservableCollection<PretixCartItem<PretixSalesItem>> Cart { get; set; } = new();
    
    public void Add(PretixSalesItem salesItem)
    {
        var cartItem = Cart.FirstOrDefault(x => x.Item.Id == salesItem.Id);
        
        if (cartItem != null)
        {
            cartItem.Count += Amount;
        }
        else
        {
            var add = new PretixCartItem<PretixSalesItem>(salesItem)
            {
                Count = Amount
            };
        
            Cart.Add(add);
        }

        CartUpdated?.Invoke(null, EventArgs.Empty);
    }

    public void Remove(PretixSalesItem salesItem)
    {
        var cartItem = Cart.FirstOrDefault(x => x.Item.Id == salesItem.Id);

        if (cartItem != null)
        {
            if (cartItem.Count > 1)
            {
                cartItem.Count--;
            }
            if (cartItem.Count == 1)
            {
                Cart.Remove(cartItem);
            }
        }

        CartUpdated?.Invoke(null, EventArgs.Empty);
    }

    public void Clear()
    {
        Cart.Clear();
        CartUpdated?.Invoke(null, EventArgs.Empty);
    }

    public event EventHandler? CartUpdated;
}