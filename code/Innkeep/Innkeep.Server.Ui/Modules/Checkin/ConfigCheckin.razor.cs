using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Checkin;

public partial class ConfigCheckin : ComponentBase
{
	private PretixCheckinList? _currentCheckinList;

	[Inject]
	public ISnackbar Snackbar { get; set; }

	[Inject]
	public IPretixCheckinListRepository CheckinListRepository { get; set; } = null!;

	[Inject]
	public IDbService<PretixConfig> ConfigService { get; set; } = null!;

	private IEnumerable<PretixCheckinList> CheckinLists { get; set; } = [];

	private PretixCheckinList? CurrentCheckinList
	{
		get => _currentCheckinList;
		set
		{
			_currentCheckinList = value;

			CurrentItem.SelectedCheckinListId = value?.Id;
			CurrentItem.SelectedCheckinListName = value?.Name;
		}
	}

	private PretixConfig CurrentItem => ConfigService.CurrentItem!;

	protected override async Task OnInitializedAsync()
	{
		await ConfigService.Load();

		if (string.IsNullOrEmpty(CurrentItem.SelectedOrganizerSlug) ||
			string.IsNullOrEmpty(CurrentItem.SelectedEventSlug))
			return;

		CheckinLists = await CheckinListRepository.GetAll(
			CurrentItem.SelectedOrganizerSlug,
			CurrentItem.SelectedEventSlug
		);

		CurrentCheckinList = CheckinLists.FirstOrDefault(x => x.Id == CurrentItem.SelectedCheckinListId);

		await InvokeAsync(StateHasChanged);
	}

	private async Task Save()
	{
		var result = await ConfigService.Save();

		if (result)
			Snackbar.Add("Saved", Severity.Success);

		else
			Snackbar.Add("Error while saving", Severity.Error);
	}
}