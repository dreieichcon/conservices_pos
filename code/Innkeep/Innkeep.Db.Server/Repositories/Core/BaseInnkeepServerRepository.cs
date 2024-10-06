using Demolite.Db.Interfaces;
using Demolite.Db.Repositories;
using Innkeep.Db.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Core;

public class BaseInnkeepServerRepository<T>(IDbContextFactory<InnkeepServerContext> contextFactory)
	: AbstractBaseRepository<T, InnkeepServerContext>
	where T : class, IDbItem
{
	protected override InnkeepServerContext GetContext()
		=> contextFactory.CreateDbContext();

	protected override async Task<InnkeepServerContext> GetContextAsync()
		=> await contextFactory.CreateDbContextAsync();
}