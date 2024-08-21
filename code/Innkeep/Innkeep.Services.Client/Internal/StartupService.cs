using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Registers;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Internal;

namespace Innkeep.Services.Client.Internal;

public class StartupService : IStartupService
{
	private readonly IRegisterConnectionService _connectionService;
	private readonly IDbService<ClientConfig> _clientConfigService;

	public StartupService(IRegisterConnectionService connectionService, IDbService<ClientConfig> clientConfigService)
	{
		_connectionService = connectionService;
		_clientConfigService = clientConfigService;
	}

	public async Task OnStartup()
	{
		await _clientConfigService.Load();

		await _connectionService.Connect(_clientConfigService.CurrentItem!.HardwareIdentifier);
	}
}