using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Innkeep.Server.Db.Context;

public class InnkeepServerContextFactory : IDesignTimeDbContextFactory<InnkeepServerContext>
{
	public InnkeepServerContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<InnkeepServerContext>();
		optionsBuilder.UseSqlite("DataSource=./db/server.db");

		return new InnkeepServerContext(optionsBuilder.Options);
	}
}