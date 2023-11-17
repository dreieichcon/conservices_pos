using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Interfaces.Api;

public interface IPretixService
{
    public IEnumerable<PretixOrganizer> Organizers { get; set; }
    
    public IEnumerable<PretixEvent> Events { get; set; }
    
    public PretixOrganizer? SelectedOrganizer { get; set; }
    
    public PretixEvent? SelectedEvent { get; set; }

    public IEnumerable<PretixSalesItem> SalesItems { get; set; }
    
    public List<string> SelectedCheckinLists { get; set; }

    public IEnumerable<PretixCheckinList> CheckinLists { get; set; }

    public Task<PretixOrderResponse?> CreateOrder(PretixTransaction pretixTransaction);

    public Task<PretixCheckinResponse?> CheckIn(PretixCheckin checkin);

    public void Reload();

    public event EventHandler ItemUpdated;

    public event EventHandler Initialized;
}