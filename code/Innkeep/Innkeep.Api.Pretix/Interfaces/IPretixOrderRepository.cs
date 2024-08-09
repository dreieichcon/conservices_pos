using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrderRepository
{
	public Task<PretixOrderResponse?> CreateOrder(string pretixOrganizer, string pretixEvent, IEnumerable<DtoSalesItem> cart, bool isTestMode);
}