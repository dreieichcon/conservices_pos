using Innkeep.Client.Data.DomainModels;
using Innkeep.Client.Interfaces.Services;

namespace Innkeep.DI.Services;

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