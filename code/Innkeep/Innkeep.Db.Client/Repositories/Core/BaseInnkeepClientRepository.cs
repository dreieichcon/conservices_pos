using Demolite.Db.Interfaces;
using Demolite.Db.Repositories;
using Innkeep.Db.Client.Context;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Client.Repositories.Core;

public class BaseInnkeepClientRepository<T>(IDbContextFactory<InnkeepClientContext> contextFactory)
	: AbstractBaseRepository<T, InnkeepClientContext>
	where T : class, IDbItem
{
	protected override InnkeepClientContext GetContext()
		=> contextFactory.CreateDbContext();

	protected override async Task<InnkeepClientContext> GetContextAsync()
		=> await contextFactory.CreateDbContextAsync();
}