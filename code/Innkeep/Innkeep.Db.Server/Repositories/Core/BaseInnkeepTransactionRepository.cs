using Innkeep.Db.Interfaces;
using Innkeep.Db.Repositories;
using Innkeep.Db.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Core;

public class BaseInnkeepTransactionRepository<T>(IDbContextFactory<InnkeepTransactionContext> contextFactory) : AbstractBaseRepository<T, InnkeepTransactionContext> where T : class, IHasOperation
{
	protected override async Task<InnkeepTransactionContext> GetContextAsync()
	{
		return await contextFactory.CreateDbContextAsync();
	}

	protected override InnkeepTransactionContext GetContext()
	{
		return contextFactory.CreateDbContext();
	}
}