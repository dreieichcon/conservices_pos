using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Server.Repositories.Core;

public class AbstractServerRepository(IDbService<ClientConfig> clientConfigService) : BaseServerRepository
{
	protected async Task<string> GetAddress()
	{
		if (clientConfigService.CurrentItem is null)
			await clientConfigService.Load();

		return clientConfigService.CurrentItem!.ServerAddress;
	}

	protected Dictionary<string, string> IdentifierFormData() =>
		new()
		{
			{
				"identifier", clientConfigService.CurrentItem!.HardwareIdentifier
			},
		};
}