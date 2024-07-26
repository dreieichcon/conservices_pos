using Innkeep.Api.Server.Interfaces;
using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Serilog;

namespace Innkeep.Api.Server.Repositories.Registers;

public class RegisterConnectionRepository(IDbService<ClientConfig> clientConfigService) : BaseServerRepository, IRegisterConnectionRepository
{
	protected override int Timeout => 500;

	public async Task<bool> Test()
	{
		return await TryTest(await GetAddress());
	}

	public async Task<bool> Connect(string identifier, string description, string ip)
	{
		var uri = $"{await GetAddress()}/register/connect";

		var formData = new Dictionary<string, string>()
		{
			{
				"identifier", identifier
			},
			{
				"description", description
			},
			{
				"ip", ip
			},
		};
		
		var response = await Get(uri, formData);

		return response.IsSuccess;
	}

	public async Task<bool> Discover(string address)
	{
		return await TryTest(address);
	}

	private async Task<bool> TryTest(string address)
	{
		try
		{
			var uri = $"{address}/register/discover";
			var result = await Get(uri, 200);
			return result.IsSuccess;
		}
		catch (Exception ex)
		{
			Log.Error("Exception during Server connection Test: {Exception}", ex);
			return false;
		}
	}

	private async Task<string> GetAddress()
	{
		if (clientConfigService.CurrentItem is null)
			await clientConfigService.Load();

		return clientConfigService.CurrentItem!.ServerAddress;
	}
}