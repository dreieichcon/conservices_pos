using System.Media;
using Innkeep.Api.Enum.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Services.Client.Interfaces.Checkin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Serilog;

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

		try
		{
			var result = await CheckinService.CheckIn(CurrentScan);

			if (result is null)
				Snackbar.Add("Critical Error", Severity.Error);

			await Beep(result);
			await _flasher.Flash(result);

			CurrentScan = "";
			await InvokeAsync(StateHasChanged);
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Error while checking in!");
			await Beep(null);
			await _flasher.Flash(null);
		}
		
	}

	private static async Task Beep(PretixCheckinResponse? result)
	{
		await Task.Run(
			() =>
			{
				if (result is {Status: CheckinStatus.Ok})
				{
					Console.Beep(880, 75);
					Console.Beep(1174, 125);
				}
				else
				{
					Console.Beep(880, 75);
					Console.Beep(830, 125);
				}
			}
		);
	}
}