using Innkeep.Services.Client.Interfaces.Checkin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Innkeep.Client.Ui.Modules.Scan;

public partial class Scan
{
	[Inject]
	public ICheckinService CheckinService { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

	public string CurrentScan { get; set; } = "";
	
	private async Task CommitScan(KeyboardEventArgs obj)
	{
		if (obj.Key != "Enter")
			return;

		var result = await CheckinService.CheckIn(CurrentScan);

		if (result is null)
			Snackbar.Add("Critical Error", Severity.Error);

		await _flasher.Flash(result);
		
		CurrentScan = "";
		await InvokeAsync(StateHasChanged);
	}
}