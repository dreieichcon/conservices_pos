using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config;

public partial class ConfigFiskaly
{
	[Inject]
	public IDbService<FiskalyConfig> FiskalyConfigService { get; set; } = null!;

	private FiskalyConfig? ConfigItem => FiskalyConfigService.CurrentItem;

	protected override async Task OnInitializedAsync()
	{
		await FiskalyConfigService.Load();
		await InvokeAsync(StateHasChanged);
	}

	private async Task Save()
	{
		await FiskalyConfigService.Save();
	}
}