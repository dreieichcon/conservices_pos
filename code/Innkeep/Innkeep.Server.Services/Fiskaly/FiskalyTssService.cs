using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Db.Server.Models;
using Innkeep.Server.Services.Interfaces.Fiskaly;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Fiskaly;

public class FiskalyTssService(
	IDbService<FiskalyConfig> configService, 
	IFiskalyTssRepository tssRepository,
	IFiskalyAuthenticationService authenticationService) 
	: IFiskalyTssService
{
	public event EventHandler? ItemsUpdated;
	
	private FiskalyTss? _currentTss;

	public FiskalyTss? CurrentTss
	{
		get => _currentTss;
		set
		{
			_currentTss = value;
			configService.CurrentItem.TseId = _currentTss?.Id ?? string.Empty;
		}
	}

	public IEnumerable<FiskalyTss> TssObjects { get; set; } = new List<FiskalyTss>();

	public async Task Load()
	{
		await configService.Load();
		TssObjects = await tssRepository.GetAll();

		if (string.IsNullOrEmpty(configService.CurrentItem.TseId) || !TssObjects.Any()) 
			return;
		
		CurrentTss = TssObjects.FirstOrDefault(x => x.Id == configService.CurrentItem.TseId);
		
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> CreateNew()
	{
		var tssResult = await tssRepository.CreateTss(Guid.NewGuid().ToString());

		if (tssResult is null) return false;

		CurrentTss = tssResult;

		await authenticationService.CreateTseConfig(CurrentTss);

		return authenticationService.CurrentConfig.TseId == CurrentTss.Id && !string.IsNullOrEmpty(authenticationService.CurrentConfig.TsePuk);
	}

	public async Task<bool> Deploy()
	{
		var result = await tssRepository.DeployTss(CurrentTss!);
		return result != null;
	}

	public async Task<bool> ChangeAdminPin()
	{
		var result = await tssRepository.ChangeAdminPin(CurrentTss!);
		
		if (result)
			await Save();

		return result;
	}

	public async Task<bool> InitializeTss()
	{
		var result = await tssRepository.InitializeTss(CurrentTss!);

		if (result is null)
			return false;

		CurrentTss = result;
		await Save();

		return true;
	}

	public async Task<bool> Save()
	{
		var cfs = await configService.Save();
		var ats = await authenticationService.SaveTseConfig();

		return cfs && ats;
	}
}