using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Server.Interfaces.Pretix;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Event;

public partial class ConfigEvent
{
	private PretixEvent? _selectedEvent;

	private PretixOrganizer? _selectedOrganizer;

	[Inject]
	public IDbService<PretixConfig> PretixConfigService { get; set; } = null!;

	[Inject]
	public IPretixOrganizerRepository PretixOrganizerRepository { get; set; } = null!;

	[Inject]
	public IPretixEventRepository PretixEventRepository { get; set; } = null!;

	[Inject]
	public IPretixSalesItemService PretixSalesItemService { get; set; } = null!;

	private PretixOrganizer? SelectedOrganizer
	{
		get => _selectedOrganizer;
		set
		{
			_selectedOrganizer = value;

			if (ConfigItem is null) return;

			ConfigItem.SelectedOrganizerName = value?.Name;
			ConfigItem.SelectedOrganizerSlug = value?.Slug;

			Task.Run(async () => await LoadEvents());
		}
	}

	private PretixEvent? SelectedEvent
	{
		get => _selectedEvent;
		set
		{
			_selectedEvent = value;

			if (ConfigItem is null) return;

			ConfigItem.SelectedEventName = value?.Name.German;
			ConfigItem.SelectedEventSlug = value?.Slug;

			InvokeAsync(StateHasChanged);
		}
	}

	private PretixConfig? ConfigItem => PretixConfigService.CurrentItem;

	private IEnumerable<PretixOrganizer> AvailableOrganizers { get; set; } = new List<PretixOrganizer>();

	private IEnumerable<PretixEvent> AvailableEvents { get; set; } = new List<PretixEvent>();

	protected override async Task OnInitializedAsync()
	{
		await PretixConfigService.Load();

		AvailableOrganizers = await PretixOrganizerRepository.GetOrganizers();
		SelectedOrganizer = AvailableOrganizers.FirstOrDefault(x => x.Slug == ConfigItem?.SelectedOrganizerSlug);

		await base.OnInitializedAsync();
		await InvokeAsync(StateHasChanged);
	}

	private async Task LoadEvents()
	{
		if (SelectedOrganizer is null) return;

		AvailableEvents = await PretixEventRepository.GetEvents(SelectedOrganizer);

		SelectedEvent = AvailableEvents.FirstOrDefault(x => x.Slug == ConfigItem?.SelectedEventSlug);

		await InvokeAsync(StateHasChanged);
	}

	private async Task Save()
	{
		var result = await PretixConfigService.Save();

		if (result)
		{
			Snackbar.Add("Saved Successfully", Severity.Success);
			await PretixSalesItemService.Load();
		}

		else
		{
			Snackbar.Add("Error while saving", Severity.Error);
		}
	}
}