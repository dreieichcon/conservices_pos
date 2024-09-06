using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Server.Interfaces.Fiskaly.Tss;

namespace Innkeep.Services.Server.Fiskaly.Tss;

public partial class FiskalyTssService(
	IDbService<FiskalyConfig> configService,
	IFiskalyTssRepository tssRepository,
	IFiskalyAuthenticationService authenticationService
) : IFiskalyTssService
{
	private FiskalyTss? _currentTss;

	public event EventHandler? ItemsUpdated;

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
		var result = await tssRepository.GetAll();

		TssObjects = result.Object ?? [];

		if (string.IsNullOrEmpty(configService.CurrentItem.TseId) || !TssObjects.Any())
			return;

		CurrentTss = TssObjects.FirstOrDefault(x => x.Id == configService.CurrentItem.TseId);

		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		var ats = await authenticationService.SaveTseConfig();
		var cfs = await configService.Save();

		return cfs && ats;
	}
}