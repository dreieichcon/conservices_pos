using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;

namespace Innkeep.Services.Server.Interfaces.Pretix;

public interface IPretixOrderService
{
	public Task<PretixOrderResponse?> CreateOrder(IEnumerable<DtoSalesItem> cart);
}