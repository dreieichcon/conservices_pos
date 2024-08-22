using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Services.Server.Interfaces.Internal;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Services.Server.Internal;

namespace Innkeep.Services.Server.Pretix;

public class PretixSalesItemService : IPretixSalesItemService
{

	private readonly IPretixSalesItemRepository _salesItemRepository;
	private readonly IPretixQuotaRepository _quotaRepository;
	private readonly IRegisterService _registerService;
	private readonly IEventStateService _eventStateService;

	public PretixSalesItemService(
		IPretixSalesItemRepository salesItemRepository,
		IPretixQuotaRepository quotaRepository,
		IRegisterService registerService,
		IEventStateService eventStateService
	)
	{
		_salesItemRepository = salesItemRepository;
		_quotaRepository = quotaRepository;
		_registerService = registerService;
		_eventStateService = eventStateService;
	}

	public event EventHandler? SalesItemsUpdated;

	public IEnumerable<PretixSalesItem> SalesItems { get; set; } = new List<PretixSalesItem>();

	public IEnumerable<DtoSalesItem> DtoSalesItems { get; set; } = new List<DtoSalesItem>();

	private readonly PeriodicTimer _reloadTimer = new (TimeSpan.FromMinutes(2));
	
	public async Task ReloadTimer()
	{
		while (await _reloadTimer.WaitForNextTickAsync())
		{
			await LoadQuotas();
			await _registerService.ReloadConnected();
		}
	}

	public async Task Load()
	{
		if (!_eventStateService.IsEventConfigured) return;

		SalesItems =
			(await _salesItemRepository.GetItems(
				_eventStateService.PretixOrganizerSlug,
				_eventStateService.PretixEventSlug
			)).Where(item => item.AllSalesChannels || item.SalesChannels.Contains("pretixpos"));

		DtoSalesItems = SalesItems.Select(DtoSalesItem.FromPretix);
		_eventStateService.EventCurrency = DtoSalesItems.FirstOrDefault()?.Currency ?? "EUR";
		
		await LoadQuotas();
	}

	public async Task LoadQuotas()
	{
		var quotas = await _quotaRepository.GetAll(
			_eventStateService.PretixOrganizerSlug,
			_eventStateService.PretixEventSlug
		);

		var toUpdate = DtoSalesItems.ToList();

		foreach (var item in toUpdate)
		{
			var itemQuotas = quotas.Where(x => x.Items.Contains(item.Id)).MaxBy(x => x.Size);

			if (itemQuotas is null)
			{
				item.QuotaMax = null;
				item.QuotaLeft = null;
				item.SoldOut = false;

				continue;
			}

			item.QuotaMax = itemQuotas.Size;
			item.QuotaLeft = itemQuotas.AmountAvailable;
			item.SoldOut = itemQuotas.Closed;
		}

		DtoSalesItems = toUpdate;

		SalesItemsUpdated?.Invoke(this, EventArgs.Empty);
	}
}