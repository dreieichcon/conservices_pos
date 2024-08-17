using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Innkeep.Db.Server.Context;

public class InnkeepTransactionContextDesignFactory : IDesignTimeDbContextFactory<InnkeepTransactionContext>
{
	public InnkeepTransactionContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<InnkeepTransactionContext>();
		optionsBuilder.UseSqlite("DataSource=./db/transaction/migrations.db");

		return new InnkeepTransactionContext(optionsBuilder.Options);
	}
}