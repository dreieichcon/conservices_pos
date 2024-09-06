using System.Text;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Interfaces.General;
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
		var pretixEventSettings = await eventRepository.GetEventSettings(PretixOrganizerSlug, PretixEventSlug);

		if (pretixEvent is null || pretixEventSettings is null) return null;

		var order = await orderRepository.CreateOrder(
			PretixOrganizerSlug,
			PretixEventSlug,
			cart,
			pretixEvent.IsTestMode
		);

		if (order == null) return order;

		order.EventTitle = pretixEvent.Name.German ?? "NAME SETZEN";
		order.ReceiptHeader = CreateHeader(pretixEventSettings);

		return order;
	}

	private string CreateHeader(PretixEventSettings settings)
	{
		var sb = new StringBuilder();

		sb.AppendLine(settings.InvoiceCompanyName);
		sb.AppendLine(settings.InvoiceStreetAddress);
		sb.AppendLine($"{settings.InvoiceZipCode} {settings.InvoiceCity}");
		sb.AppendLine(settings.InvoiceCountry);

		if (!string.IsNullOrEmpty(settings.InvoiceVatId))
			sb.AppendLine($"Ust.Id: {settings.InvoiceVatId}");

		return sb.ToString();
	}
}