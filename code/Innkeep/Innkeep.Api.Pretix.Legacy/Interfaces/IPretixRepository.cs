using Innkeep.Api.Pretix.Legacy.Models.Internal;
using Innkeep.Api.Pretix.Legacy.Models.Objects;

namespace Innkeep.Api.Pretix.Legacy.Interfaces;

public interface IPretixRepository
{
    public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
    public Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer);
    public Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer organizer, PretixEvent pretixEvent);
	
	public Task<IEnumerable<PretixCheckinList>> GetCheckinLists(PretixOrganizer organizer, PretixEvent pretixEvent);

	public Task<PretixOrderResponse?> CreateOrder
	(PretixOrganizer organizer,
	PretixEvent pretixEvent,
	IEnumerable<PretixCartItem<PretixSalesItem>> cartItems,
	bool isTest = false);
	
	public Task<PretixCheckinResponse?> CheckIn(PretixOrganizer organizer, PretixCheckin pretixCheckin);

}