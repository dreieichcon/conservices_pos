using Innkeep.Db.Interfaces;
using Innkeep.Server.Db.Context;
using Innkeep.Server.Db.Models;
using Innkeep.Server.Db.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Db.Repositories.Registers;

public class RegisterRepository(IDbContextFactory<InnkeepServerContext> contextFactory) 
	: BaseInnkeepServerRepository<Register>(contextFactory), IDbRepository<Register>
{
	
}