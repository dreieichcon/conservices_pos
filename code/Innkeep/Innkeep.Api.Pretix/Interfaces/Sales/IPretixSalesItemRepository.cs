using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Api.Pretix.Interfaces.Sales;

public interface IPretixSalesItemRepository
{
	public Task<IHttpResponse<IEnumerable<PretixSalesItem>>> GetItems(string organizerSlug, string eventSlug);
}