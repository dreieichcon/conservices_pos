using Innkeep.Services.Interfaces.Db;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Context;

public class InnkeepTransactionContextFactory(ITransactionDbSettingsService settingsService)
	: IDbContextFactory<InnkeepTransactionContext>
{
	public InnkeepTransactionContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<InnkeepTransactionContext>();
		optionsBuilder.UseSqlite($"DataSource={settingsService.CurrentConnectionString}");

		return new InnkeepTransactionContext(optionsBuilder.Options);
	}
}