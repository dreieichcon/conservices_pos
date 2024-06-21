using Innkeep.Db.Interfaces;
using Innkeep.Db.Repositories;
using Innkeep.Server.Db.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Db.Repositories.Core;

public class BaseInnkeepServerRepository<T>(IDbContextFactory<InnkeepServerContext> contextFactory) : AbstractBaseRepository<T, InnkeepServerContext> where T : class, IHasOperation
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