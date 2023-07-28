using Innkeep.Client.Interfaces.Services;

namespace Innkeep.DI.Services;

public class ClientServerConnectionService : IClientServerConnectionService
{
	private readonly IClientSettingsService _clientSettingsService;

	public ClientServerConnectionService(IClientSettingsService clientSettingsService)
	{
		_clientSettingsService = clientSettingsService;
	}

	public bool TestConnection()
	{
		return true;
	}

	public bool AutoDiscover(out Uri? uri)
	{
		uri = null;
		return false;
	}
}