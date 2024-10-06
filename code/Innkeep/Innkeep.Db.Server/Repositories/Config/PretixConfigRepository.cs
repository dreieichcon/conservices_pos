using Demolite.Db.Interfaces;
using Innkeep.Db.Server.Context;
using Innkeep.Db.Server.Models.Config;
using Innkeep.Db.Server.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Config;

public class PretixConfigRepository(IDbContextFactory<InnkeepServerContext> contextFactory)
	: BaseInnkeepServerRepository<PretixConfig>(contextFactory), IDbRepository<PretixConfig>
{
}