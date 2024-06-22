using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Server.Db.Models;
using Innkeep.Server.Services.Interfaces.Fiskaly;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Fiskaly;

public class FiskalyTssService(IDbService<FiskalyConfig> configService, IFiskalyTssRepository tssRepository) 
	: IFiskalyTssService
{
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
	}

	public async Task<bool> Save() 
		=> await configService.Save();
}