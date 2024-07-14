using Innkeep.Db.Client.Context;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Client.Repositories.Core;

public class BaseInnkeepClientRepository<T>(IDbContextFactory<InnkeepClientContext> contextFactory) : AbstractBaseRepository<T, InnkeepClientContext> where T : class, IHasOperation
{
	protected override InnkeepClientContext GetContext()
	{
		return contextFactory.CreateDbContext();
	}

	protected override async Task<InnkeepClientContext> GetContextAsync()
	{
		return await contextFactory.CreateDbContextAsync();
	}
}