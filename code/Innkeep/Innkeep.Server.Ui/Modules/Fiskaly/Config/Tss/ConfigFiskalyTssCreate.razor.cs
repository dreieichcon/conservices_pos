using Innkeep.Services.Server.Interfaces.Fiskaly;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config.Tss;

public partial class ConfigFiskalyTssCreate
{
	[Inject]
	public IFiskalyTssService TssService { get; set; } = null!;
	
	[Inject]
	public NavigationManager NavManager { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

	public async Task Create()
	{
		var result = await TssService.CreateNew();

		if (result)
		{
			Snackbar.Add("Created new TSS!", Severity.Success);
			NavManager.NavigateTo("/config/fiskaly/tss");
		}
		else
		{
			Snackbar.Add("Creating the TSS failed. Please check your logs", Severity.Error);
		}
	}
}