using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Innkeep.Client.Db.Context;

public class InnkeepClientContextFactory: IDesignTimeDbContextFactory<InnkeepClientContext>
{
	public InnkeepClientContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<InnkeepClientContext>();
		optionsBuilder.UseSqlite("DataSource=./db/client.db");

		return new InnkeepClientContext(optionsBuilder.Options);
	}
}