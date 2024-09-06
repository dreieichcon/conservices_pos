using Innkeep.Api.Internal.Interfaces.Server.Register;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Serilog;

namespace Innkeep.Api.Internal.Repositories.Server.Register;

public class RegisterConnectionRepository(IDbService<ClientConfig> clientConfigService) : AbstractServerRepository(clientConfigService), IRegisterConnectionRepository
{
	protected override int Timeout => 500;

	public async Task<bool> Test()
	{
		return await TryTest(await GetAddress());
	}

	public async Task<bool> Connect(string identifier, string description, string ip)
	{
		var serverAddress = await GetAddress();
		var uri = $"{serverAddress}/register/connect";

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

		try
		{
			var response = await Get(uri, formData);

			return response.IsSuccess;
		}
		catch (Exception ex)
		{
			Log.Warning("Server {ServerAddress} does not seem to be running: {Exception}", serverAddress, ex.Message);
			return false;
		}
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
			Log.Error(ex, "Exception during Server connection Test");
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