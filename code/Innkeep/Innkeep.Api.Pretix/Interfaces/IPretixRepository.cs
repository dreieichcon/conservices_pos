using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixRepository
{
    public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
    public Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer);
    public Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer organizer, PretixEvent pretixEvent);

	public Task<PretixOrderResponse> CreateOrder
	(PretixOrganizer organizer,
	PretixEvent pretixEvent,
	IEnumerable<PretixCartItem<PretixSalesItem>> cartItems,
	bool isTest = false);
	
	public Task<bool> CheckIn(PretixOrganizer organizer, PretixOrderResponse orderResponse);

}