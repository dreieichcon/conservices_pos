using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrderRepository
{
	public Task<IHttpResponse<PretixOrderResponse>> CreateOrder(
		string pretixOrganizer,
		string pretixEvent,
		IEnumerable<DtoSalesItem> cart,
		bool isTestMode
	);
}