using Innkeep.Db.Interfaces;
using Innkeep.Db.Repositories;
using Innkeep.Db.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Core;

public class BaseInnkeepServerRepository<T>(IDbContextFactory<InnkeepServerContext> contextFactory)
	: AbstractBaseRepository<T, InnkeepServerContext> where T : class, IHasOperation
{
	protected override InnkeepServerContext GetContext()
	{
		return contextFactory.CreateDbContext();
	}

	protected override async Task<InnkeepServerContext> GetContextAsync()
	{
		return await contextFactory.CreateDbContextAsync();
	}
}