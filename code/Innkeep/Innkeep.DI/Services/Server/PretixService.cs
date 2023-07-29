using Innkeep.Core.DomainModels.Print;
using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Server.Pretix.Interfaces;
using Innkeep.Shared.Objects.Transaction;
using Serilog;

namespace Innkeep.DI.Services;

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

	public async Task<Receipt?> CreateOrder(Transaction transaction)
	{
		var response = await _pretixRepository.CreateOrder(SelectedOrganizer!, SelectedEvent!, transaction.TransactionItems);

		return CreateReceiptFromResponse(response, transaction);
	}

	private Receipt CreateReceiptFromResponse(PretixOrderResponse orderResponse, Transaction transaction)
	{
		var receipt = new Receipt();
		
		// TODO - Add this to receipt class directly.

		var name = SelectedEvent.Name.German;
		
		receipt.Lines.Add(new PrintInfo(name, LineType.Title));
		receipt.Lines.Add(new("", LineType.Blank));
		receipt.Lines.Add(new("", LineType.Divider));

		foreach (var item in transaction.TransactionItems)
		{
			receipt.Lines.Add(new(item.LineInfo(SelectedEvent.Currency), LineType.Line));	
			receipt.Lines.Add(new("", LineType.Blank));
		}
		
		receipt.Lines.Add(new("", LineType.Blank));
		receipt.Lines.Add(new("", LineType.Divider));
		
		receipt.Lines.Add(new PrintInfo($"Summe: {transaction.Sum.ToString().PadLeft(5, ' ')}€", LineType.Sum));
		receipt.Lines.Add(new PrintInfo($"Erhalten: {transaction.AmountGiven.ToString().PadLeft(5, ' ')}€", LineType.Sum));
		receipt.Lines.Add(new PrintInfo($"Rück: {transaction.Return.ToString().PadLeft(5, ' ')}€", LineType.Sum));
		
		receipt.Lines.Add(new("", LineType.Blank));
		receipt.Lines.Add(new("", LineType.Divider));
		
		receipt.Lines.Add(new($"Order ID: {orderResponse.Code}", LineType.Center));
		
		receipt.Lines.Add(new($"Datum / Uhrzeit:", LineType.Line));
		receipt.Lines.Add(new($"{DateTime.Now.ToString()}", LineType.Line));

		return receipt;
	}
}