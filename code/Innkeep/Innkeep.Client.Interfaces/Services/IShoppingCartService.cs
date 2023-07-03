using System.Collections.ObjectModel;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Interfaces.Services;

public interface IShoppingCartService
{
    public int Amount { get; set; }
    
    public ObservableCollection<PretixCartItem<PretixSalesItem>> Cart { get; set; }
    
    void Add(PretixSalesItem salesItem);

    void Remove(PretixSalesItem salesItem);

    void Clear();

    public event EventHandler CartUpdated;
}