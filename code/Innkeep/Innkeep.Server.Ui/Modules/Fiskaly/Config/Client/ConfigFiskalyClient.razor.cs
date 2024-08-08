using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Services.Interfaces.Hardware;
using Innkeep.Services.Server.Interfaces.Fiskaly;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Serilog;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config.Client;

public partial class ConfigFiskalyClient
{
	[Inject]
	public IFiskalyClientService ClientService { get; set; } = null!;

	[Inject]
	public IFiskalyTssService TssService { get; set; } = null!;

	[Inject]
	public IHardwareService HardwareService { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

	private IEnumerable<FiskalyClient> Clients => ClientService.Clients;

	private FiskalyClient? CurrentClient
	{
		get => ClientService.CurrentClient;
		set => ClientService.CurrentClient = value;
	}

	private async Task Reload()
	{
		await OnInitializedAsync();
	}
	
	private async Task ActivateClient()
	{
		var result = await ClientService.Activate();

		if (result)
		{
			Snackbar.Add("Activated Client", Severity.Success);

			Log.Information(
				"Client {ClientSerial} activated for Tss {TssId} by {User}",
				CurrentClient?.SerialNumber,
				CurrentClient?.TssId,
				Environment.UserName
			);
		}
		else
		{
			Snackbar.Add("Error while activating Client. Please check your logs.", Severity.Error);
		}
		
		await InvokeAsync(StateHasChanged);
	}

	private async Task DeactivateClient()
	{
		var result = await ClientService.Deactivate();

		if (result)
		{
			Snackbar.Add("Deactivated Client", Severity.Success);

			Log.Information(
				"Client {ClientSerial} deactivated for Tss {TssId} by {User}",
				CurrentClient?.SerialNumber,
				CurrentClient?.TssId,
				Environment.UserName
			);
		}
		else
		{
			Snackbar.Add("Error while deactivating Client. Please check your logs.", Severity.Error);
		}
		
		await InvokeAsync(StateHasChanged);
	}

	private async Task CreateClient()
	{
		var result = await ClientService.CreateNew();

		if (result)
		{
			Snackbar.Add("Created new Client", Severity.Success);

			Log.Information(
				"Client {ClientSerial} created for Tss {TssId} by {User}",
				CurrentClient?.SerialNumber,
				CurrentClient?.TssId,
				Environment.UserName
			);
		}
		else
		{
			Snackbar.Add("Error while creating new Client. Please check your logs.", Severity.Error);
		}

		await InvokeAsync(StateHasChanged);
	}

	private async Task Save()
	{
		var result = await ClientService.Save();

		if (result)
		{
			Snackbar.Add("Saved", Severity.Success);

			Log.Information(
				"Client {ClientSerial} set as active client for Server {ServerId} by {User}",
				CurrentClient?.SerialNumber,
				HardwareService.ClientIdentifier,
				Environment.UserName
			);
		}

		else
			Snackbar.Add("Error while saving.", Severity.Error);

		await InvokeAsync(StateHasChanged);
	}

	
}