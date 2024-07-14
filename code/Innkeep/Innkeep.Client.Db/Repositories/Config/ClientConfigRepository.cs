using Innkeep.Client.Db.Context;
using Innkeep.Client.Db.Models;
using Innkeep.Client.Db.Repositories.Core;
using Innkeep.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Client.Db.Repositories.Config;

public class ClientConfigRepository(IDbContextFactory<InnkeepClientContext> contextFactory)
	: BaseInnkeepClientRepository<ClientConfig>(contextFactory), IDbRepository<ClientConfig>
{
	
}