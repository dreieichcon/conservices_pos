using Microsoft.EntityFrameworkCore;

namespace Innkeep.Client.Db.Context;

public class InnkeepClientContext(DbContextOptions<InnkeepClientContext> options) : DbContext(options)
{
	
}