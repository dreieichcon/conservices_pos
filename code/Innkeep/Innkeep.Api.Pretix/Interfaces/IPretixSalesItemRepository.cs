using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixSalesItemRepository
{
	public Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer pOrganizer, PretixEvent pEvent);

	public Task<IEnumerable<PretixSalesItem>> GetItems(string pOrganizerSlug, string pEventSlug);
}