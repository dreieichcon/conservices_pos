using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Innkeep.Client.Ui.Modules.Config;

public partial class Config
{
	[Inject]
	public IDbService<ClientConfig> ClientConfigService { get; set; } = null!;

	public ClientConfig? CurrentConfig => ClientConfigService.CurrentItem;
	
	protected override async Task OnInitializedAsync()
	{
		await ClientConfigService.Load();
		await InvokeAsync(StateHasChanged);
	}
}