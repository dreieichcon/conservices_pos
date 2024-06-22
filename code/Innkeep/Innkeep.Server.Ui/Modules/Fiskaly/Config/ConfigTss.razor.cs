using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Server.Services.Interfaces.Fiskaly;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config;

public partial class ConfigTss
{
	[Inject]
	public IFiskalyTssService TssService { get; set; } = null!;
	
	[Inject]
	public ISnackbar Snackbar { get; set; }

	private IEnumerable<FiskalyTss> TssObjects => TssService.TssObjects;

	private FiskalyTss? CurrentTss
	{
		get => TssService.CurrentTss;
		set
		{
			TssService.CurrentTss = value;
		}
	}

	private async Task Save()
	{
		var result = await TssService.Save();

		if (result)
			Snackbar.Add("Saved", Severity.Success);
		else
			Snackbar.Add("Error while saving.", Severity.Error);
	}
}