using Innkeep.Api.Internal.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Core;

public class AbstractServerRepository(IDbService<ClientConfig> clientConfigService) : AbstractInnkeepRepository
{
	protected string Identifier => clientConfigService.CurrentItem!.HardwareIdentifier;

	protected async Task<string> GetAddress()
	{
		if (clientConfigService.CurrentItem is null)
			await clientConfigService.Load();

		return clientConfigService.CurrentItem!.ServerAddress;
	}
}