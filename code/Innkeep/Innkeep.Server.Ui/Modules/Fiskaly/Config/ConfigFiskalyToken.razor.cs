using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config;

public partial class ConfigFiskalyToken
{
	[Inject]
	public IDbService<FiskalyConfig> FiskalyConfigService { get; set; } = null!;
	
	[Inject]
	public IFiskalyAuthRepository AuthRepository { get; set; } = null!;
	
	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

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

	private async Task Test()
	{
		try
		{
			var result = await AuthRepository.Authenticate(
				new AuthenticationInfo()
				{
					Key = ConfigItem!.ApiKey,
					Secret = ConfigItem!.ApiSecret,
				}
			);

			if (result is not null)
			{
				Snackbar.Add("Success", Severity.Success);
				return;
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add("Error", Severity.Error);
		}
		
		Snackbar.Add("Invalid Credentials", Severity.Error);
	}
}