using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Client.Interfaces.Pos;
using Innkeep.Services.Client.Interfaces.Registers;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Client.Ui.Modules.Config;

public partial class Config
{
	[Inject]
	public IHardwareService HardwareService { get; set; } = null!;
	
	[Inject]
	public IDbService<ClientConfig> ClientConfigService { get; set; } = null!;

	[Inject]
	public IRegisterConnectionService RegisterConnectionService { get; set; } = null!;

	[Inject]
	public ISalesItemService SalesItemService { get; set; } = null!;

	[Inject]
	public IPrinterService PrinterService { get; set; } = null!;

	public CancellationTokenSource TokenSource { get; set; } = new();

	[Inject]
	public ISnackbar Bar { get; set; } = null!;

	private bool _isDiscovering;

	public ClientConfig? CurrentConfig => ClientConfigService.CurrentItem;
	
	protected override async Task OnInitializedAsync()
	{
		await ClientConfigService.Load();
		RegisterConnectionService.TestAddressChanged += (sender, args) => InvokeAsync(StateHasChanged);
		await InvokeAsync(StateHasChanged);
	}

	private async Task Test()
	{
		if (await RegisterConnectionService.Test())
		{
			Bar.Add("Server Exists", Severity.Success);
			return;
		}

		Bar.Add("Server does not exist.", Severity.Error);
	}

	private async Task Discover()
	{
		_isDiscovering = true;
		TokenSource.TryReset();
		
		var result = await RegisterConnectionService.Discover(TokenSource.Token);

		if (result is not null)
		{
			CurrentConfig!.ServerAddress = result;
			Bar.Add("Located Server", Severity.Success);
			_isDiscovering = false;
			return;
		}
		
		_isDiscovering = false;
		Bar.Add("Could not find a running server.", Severity.Error);
	}

	private async Task Cancel()
	{
		await TokenSource.CancelAsync();
		TokenSource.Dispose();
		TokenSource = new CancellationTokenSource();
	}
	
	private async Task Connect()
	{
		var description = ClientConfigService.CurrentItem!.ClientName;

		var result = await RegisterConnectionService.Connect(description);

		if (result)
		{
			Bar.Add("Connected successfully!", Severity.Success);
		}
		else
		{
			Bar.Add("Error while connecting. Make sure your client is trusted!", Severity.Error);
		}
	}

	private async Task Save()
	{
		var result = await ClientConfigService.Save();

		if (result)
		{
			Bar.Add("Saved", Severity.Success);
		}
	}

	private void TestPrinter()
	{
		if (!string.IsNullOrEmpty(CurrentConfig!.PrinterName))
		{
			PrinterService.TestPrint(CurrentConfig!.PrinterName);
		}
	}
}