using Demolite.Db.Interfaces;
using Demolite.Db.Repositories;
using Innkeep.Db.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Core;

public class BaseInnkeepTransactionRepository<T>(IDbContextFactory<InnkeepTransactionContext> contextFactory)
	: AbstractBaseRepository<T, InnkeepTransactionContext>
	where T : class, IDbItem
{
	protected override async Task<InnkeepTransactionContext> GetContextAsync()
		=> await contextFactory.CreateDbContextAsync();

	protected override InnkeepTransactionContext GetContext()
		=> contextFactory.CreateDbContext();
}