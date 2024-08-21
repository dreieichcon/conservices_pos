using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Internal;
using Innkeep.Services.Server.Interfaces.Pretix;

namespace Innkeep.Services.Server.Internal;

public class StartupService(IDbService<PretixConfig> pretixConfigService, IPretixSalesItemService salesItemService)
	: IStartupService
{
	public async Task OnStartup()
	{
		await pretixConfigService.Load();
		await salesItemService.Load();
	}
}