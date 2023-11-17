using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;

namespace Innkeep.Api.Pretix.Interfaces;

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