using Innkeep.Api.Client.Interfaces;
using Innkeep.Api.Client.Repositories.Core;
using Innkeep.Api.Endpoints;

namespace Innkeep.Api.Client.Repositories.Reload;

public class ClientReloadRepository : AbstractClientRepository, IClientReloadRepository
{
	public async Task<bool> Reload(string identifier, string address)
	{
		var uri = new ClientEndpointBuilder(address).WithClient().Reload().WithIdentifier(identifier).Build();

		var result = await Get(uri);

		return result.IsSuccess;
	}
}