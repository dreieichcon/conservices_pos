using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Server.Interfaces.Pretix;
using Serilog;

namespace Innkeep.Services.Server.Pretix;

public class PretixOrderService(
	IDbService<PretixConfig> configService,
	IPretixEventRepository eventRepository,
	IPretixOrderRepository orderRepository
) : IPretixOrderService
{
	private string? PretixEventSlug => configService.CurrentItem!.SelectedEventSlug;

	private string? PretixOrganizerSlug => configService.CurrentItem!.SelectedOrganizerSlug;
	
	public async Task<PretixOrderResponse?> CreateOrder(IEnumerable<DtoSalesItem> cart)
	{
		if (string.IsNullOrEmpty(PretixOrganizerSlug) || string.IsNullOrEmpty(PretixEventSlug))
		{
			Log.Error("Please make sure to select a Pretix organizer and Pretix event before creating orders");
			return null;
		}

		var pretixEvent = await eventRepository.GetEvent(PretixOrganizerSlug, PretixEventSlug);

		if (pretixEvent is null) return null;

		var order = await orderRepository.CreateOrder(
			PretixOrganizerSlug,
			PretixEventSlug,
			cart,
			pretixEvent.IsTestMode
		);

		return order;
	}
}