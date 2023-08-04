using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Interfaces;
using Innkeep.Server.Services.Interfaces.Db;

namespace Innkeep.Server.Services.Services;

public class PretixService : IPretixService
{
	private readonly IApplicationSettingsService _applicationSettingsService;
	private readonly IPretixRepository _pretixRepository;
	private PretixOrganizer? _selectedOrganizer;
	private PretixEvent? _selectedEvent;

	public PretixService(IApplicationSettingsService applicationSettingsService, IPretixRepository pretixRepository)
	{
		_applicationSettingsService = applicationSettingsService;
		_pretixRepository = pretixRepository;
		LoadFromSettings();
	}

	public IEnumerable<PretixOrganizer> Organizers { get; set; } = new List<PretixOrganizer>();

	public IEnumerable<PretixEvent> Events { get; set; } = new List<PretixEvent>();

	public PretixOrganizer? SelectedOrganizer
	{
		get => _selectedOrganizer;
		set
		{
			_selectedOrganizer = value;
			LoadEvent();
		}
	}

	public PretixEvent? SelectedEvent
	{
		get => _selectedEvent;
		set
		{
			_selectedEvent = value;
			LoadSalesItems();
		}
	}

	public IEnumerable<PretixSalesItem> SalesItems { get; set; } = new List<PretixSalesItem>();

	public void Reload()
	{
		LoadOrganizer();
	}

	public event EventHandler? ItemUpdated;

	public event EventHandler? Initialized;

	private void LoadFromSettings()
	{
		LoadOrganizer();
	}

	private void LoadOrganizer()
	{
		var selectedOrganizer = _applicationSettingsService.ActiveSetting.SelectedOrganizer;
		Organizers = Task.Run(() => _pretixRepository.GetOrganizers()).Result;
		
		if (selectedOrganizer == null) return;

		SelectedOrganizer = Organizers.FirstOrDefault(x => x.Slug == selectedOrganizer.Slug);
		ItemUpdated?.Invoke(nameof(SelectedOrganizer), EventArgs.Empty);
	}

	private void LoadEvent()
	{
		var selectedEvent = _applicationSettingsService.ActiveSetting.SelectedEvent;

		if (SelectedOrganizer == null) return;

		Events = Task.Run(() => _pretixRepository.GetEvents(SelectedOrganizer)).Result;

		if (selectedEvent == null) return;

		SelectedEvent = Events.FirstOrDefault(x => x.Slug == selectedEvent.Slug);
		ItemUpdated?.Invoke(nameof(SelectedEvent), EventArgs.Empty);
	}

	private void LoadSalesItems()
	{
		if (SelectedOrganizer == null || SelectedEvent == null) return;

		SalesItems = Task.Run(() => _pretixRepository.GetItems(SelectedOrganizer, SelectedEvent)).Result;
		ItemUpdated?.Invoke(nameof(SalesItems), EventArgs.Empty);
	}

	public async Task<PretixOrderResponse> CreateOrder(PretixTransaction pretixTransaction)
	{
		return await _pretixRepository.CreateOrder(SelectedOrganizer!, SelectedEvent!, pretixTransaction.TransactionItems);
	}
}