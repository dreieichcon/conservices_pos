using Innkeep.Client.Services.Interfaces.Register;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Client.Ui.Modules.Config;

public partial class Config
{
	[Inject]
	public IDbService<ClientConfig> ClientConfigService { get; set; } = null!;

	[Inject]
	public IRegisterConnectionService RegisterConnectionService { get; set; } = null!;

	[Inject]
	public ISnackbar Bar { get; set; } = null!;

	public ClientConfig? CurrentConfig => ClientConfigService.CurrentItem;
	
	protected override async Task OnInitializedAsync()
	{
		await ClientConfigService.Load();
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
}