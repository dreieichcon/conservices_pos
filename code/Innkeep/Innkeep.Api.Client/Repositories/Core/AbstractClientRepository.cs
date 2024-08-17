using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Services.Server.Interfaces.Registers;

namespace Innkeep.Api.Client.Repositories.Core;

public class AbstractClientRepository(IRegisterService registerService) : BaseServerRepository
{
	protected async Task<string> GetAddress(string clientId)
	{
		await registerService.Load();
#if DEBUG
		return "https://localhost:42069";
# endif
		return $"https://{registerService.KnownRegisters.First(x => x.Id == clientId).RegisterIp}:42069";
	}
}