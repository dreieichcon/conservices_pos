using Innkeep.Api.Auth;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Services.Server.Interfaces.Fiskaly.Tss;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config.Tss;

public partial class ConfigFiskalyTss
{
	[Inject]
	public IFiskalyTssService TssService { get; set; } = null!;

	[Inject]
	public IFiskalyAuthenticationService AuthenticationService { get; set; } = null!;

	[Inject]
	public NavigationManager NavManager { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; }

	public bool TaskInProgress { get; set; }

	public string TaskDescription { get; set; } = string.Empty;

	private IEnumerable<FiskalyTss> TssObjects => TssService.TssObjects;

	private FiskalyTss? CurrentTss
	{
		get => TssService.CurrentTss;
		set => TssService.CurrentTss = value;
	}

    private string TssPuk { get; set; } = string.Empty;

	private async Task Save()
	{
		var result = await TssService.Save();

		if (result)
			Snackbar.Add("Saved", Severity.Success);
		else
			Snackbar.Add("Error while saving.", Severity.Error);
	}

	private void Edit()
	{
		NavManager.NavigateTo($"/config/fiskaly/tss/{CurrentTss!.Id}");
	}

	public async Task Deploy()
	{
		TaskInProgress = true;
		TaskDescription = "Deploying Tss. This can take some time, please be patient.";

		var result = await TssService.Deploy();

		TaskInProgress = false;
		TaskDescription = string.Empty;

		if (result)
			Snackbar.Add("Successfully deployed Tss.", Severity.Success);
		else
			Snackbar.Add("Error while deploying.", Severity.Error);

		await Reload();
	}

	private async Task Reload()
	{
		await TssService.Load();
		await InvokeAsync(StateHasChanged);
	}

	private async Task Disable()
    {
        var result = await TssService.DisableTss();
        SetSnackbar(result, "Successfully disabled Tss.", "Error while disabling Tss.");
    }

	private async Task Initialize()
	{
		var result = await TssService.InitializeTss();
		SetSnackbar(result, "Successfully initialized Tss.", "Error while initializing Tss.");
	}

	private async Task SetAdminPin()
	{
		var result = await TssService.ChangeAdminPin();
		SetSnackbar(result, "Successfully set Tss admin pin.", "Error while setting admin pin.");
	}

	private void SetSnackbar(bool success, string successMessage, string failureMessage)
	{
		if (success)
			Snackbar.Add(successMessage, Severity.Success);
		else
			Snackbar.Add(failureMessage, Severity.Error);
	}

    private async Task SaveTssPuk()
    {
        if (string.IsNullOrEmpty(AuthenticationService.CurrentConfig.TsePuk))
            AuthenticationService.CurrentConfig.TsePuk = TssPuk;

        await Save();
    }
}