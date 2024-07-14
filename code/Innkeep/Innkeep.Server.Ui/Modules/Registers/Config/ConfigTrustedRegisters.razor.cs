using Innkeep.Server.Services.Interfaces.Registers;
using Microsoft.AspNetCore.Components;

namespace Innkeep.Server.Ui.Modules.Registers.Config;

public partial class ConfigTrustedRegisters
{
	[Inject]
	public IRegisterService RegisterService { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		RegisterService.ItemsUpdated += async (_, _) => await InvokeAsync(StateHasChanged);
		await RegisterService.Load();
	}

	private async Task Trust(string identifier)
	{
		await RegisterService.AddToKnown(identifier);
	}

	private async Task Save()
	{
		await RegisterService.Save();
	}
}