using Innkeep.Db.Server.Context;
using Innkeep.Services.Interfaces.Db;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Startup.Database;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Startup;

public static class ServerServiceInitializer
{
	public static async Task InitializeServices(IServiceProvider provider)
	{
		var salesItemService = provider.GetRequiredService<IPretixSalesItemService>();
		await provider.GetRequiredService<IRegisterService>().Load();

		Task.Run(async () => await salesItemService.ReloadTimer());

		var transactionSettingsService = provider.GetRequiredService<ITransactionDbSettingsService>();
		transactionSettingsService.LoadSettings();

		if (transactionSettingsService.DbExists)
		{
			var factory = provider.GetRequiredService<IDbContextFactory<InnkeepTransactionContext>>();
			DatabaseCreator.EnsureDbCreated(factory);
		}
	}
}