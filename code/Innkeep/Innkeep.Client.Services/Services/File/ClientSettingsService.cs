using Innkeep.Client.Services.Interfaces.File;
using Innkeep.Client.Services.Models;

namespace Innkeep.Client.Services.Services.File;

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