using Innkeep.Core.DomainModels.Pretix;

namespace Innkeep.Core.Interfaces.Pretix;

public interface IPretixRepository
{
    public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
    public Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer);
    public Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer organizer, PretixEvent pretixEvent);
    
}