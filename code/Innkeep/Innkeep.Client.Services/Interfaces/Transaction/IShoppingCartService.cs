using System.Collections.ObjectModel;
using Innkeep.Api.Pretix.Legacy.Models.Internal;
using Innkeep.Api.Pretix.Legacy.Models.Objects;

namespace Innkeep.Client.Services.Interfaces.Transaction;

public interface IShoppingCartService
{
    public int Amount { get; set; }
    
    public ObservableCollection<PretixCartItem<PretixSalesItem>> Cart { get; set; }
    
    void Add(PretixSalesItem salesItem);

    void Remove(PretixSalesItem salesItem);

    void Clear();

    public event EventHandler CartUpdated;
}