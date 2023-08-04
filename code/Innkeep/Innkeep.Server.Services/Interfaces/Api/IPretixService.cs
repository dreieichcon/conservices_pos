using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Interfaces;

public interface IPretixService
{
    public IEnumerable<PretixOrganizer> Organizers { get; set; }
    
    public IEnumerable<PretixEvent> Events { get; set; }
    
    public PretixOrganizer? SelectedOrganizer { get; set; }
    
    public PretixEvent? SelectedEvent { get; set; }

    public IEnumerable<PretixSalesItem> SalesItems { get; set; }

    public Task<PretixOrderResponse> CreateOrder(PretixTransaction pretixTransaction);

    public void Reload();

    public event EventHandler ItemUpdated;

    public event EventHandler Initialized;
}