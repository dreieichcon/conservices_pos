using Innkeep.Client.Services.Legacy.Interfaces.File;
using Innkeep.Client.Services.Legacy.Models;

namespace Innkeep.Client.Services.Legacy.Services.File;

public class ClientSettingsService : IClientSettingsService
{
	private readonly IClientSettingsRepository _clientSettingsRepository;

	public ClientSettingsService(IClientSettingsRepository clientSettingsRepository)
	{
		_clientSettingsRepository = clientSettingsRepository;
		Load();
	}

	public ClientSetting Setting { get; set; } = null!;

	public void Save()
	{
		_clientSettingsRepository.Save(Setting);
	}

	public void Load()
	{
		Setting = _clientSettingsRepository.Get();
	}
}