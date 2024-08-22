using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Context;
using Innkeep.Db.Server.Models;
using Innkeep.Db.Server.Models.Server;
using Innkeep.Db.Server.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Registers;

public class RegisterRepository(IDbContextFactory<InnkeepServerContext> contextFactory) 
	: BaseInnkeepServerRepository<Register>(contextFactory), IDbRepository<Register>
{
	
}