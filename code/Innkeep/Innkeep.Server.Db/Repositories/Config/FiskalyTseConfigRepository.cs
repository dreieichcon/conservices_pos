using Innkeep.Db.Interfaces;
using Innkeep.Server.Db.Context;
using Innkeep.Server.Db.Models;
using Innkeep.Server.Db.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Db.Repositories.Config;

public class FiskalyTseConfigRepository(IDbContextFactory<InnkeepServerContext> contextFactory) 
	: BaseInnkeepServerRepository<FiskalyTseConfig>(contextFactory), IDbRepository<FiskalyTseConfig>
{
	
}