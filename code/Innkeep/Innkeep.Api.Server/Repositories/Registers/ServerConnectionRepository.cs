using Innkeep.Api.Server.Interfaces;
using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Server.Repositories.Registers;

public class ServerConnectionRepository(IDbService<ClientConfig> clientConfigService, HttpClientHandler handler) 
	: BaseServerRepository(handler), IRegisterConnectionRepository
{
	
	public async Task<bool> Test()
	{
		var uri = $"{await GetAddress()}/register/discover";
		var result = await Get(uri);
		return result.IsSuccess;
	}

	public async Task<bool> Connect()
	{
		return false;
	}

	public async Task<bool> Discover()
	{
		return false;
	}

	private async Task<string> GetAddress()
	{
		if (clientConfigService.CurrentItem is null)
			await clientConfigService.Load();

		return clientConfigService.CurrentItem!.ServerAddress;
	}
}