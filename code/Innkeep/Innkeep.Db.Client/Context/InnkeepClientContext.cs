using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Client.Context;

public class InnkeepClientContext(DbContextOptions<InnkeepClientContext> options) : DbContext(options)
{
	
}