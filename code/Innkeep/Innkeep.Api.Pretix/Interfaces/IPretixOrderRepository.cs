using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrderRepository
{
	public Task<IHttpResponse<PretixOrderResponse>> CreateOrder(
		string organizerSlug,
		string eventSlug,
		IEnumerable<DtoSalesItem> cart,
		bool isTestMode
	);
}