using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Context;

public class InnkeepServerContext : DbContext
{
	public InnkeepServerContext()
	{
	}
	
	public InnkeepServerContext(DbContextOptions options) : base(options)
	{
		
	}

	public static InnkeepServerContext Create()
	{
		var optionsBuilder = new DbContextOptionsBuilder<InnkeepServerContext>();
		optionsBuilder.UseSqlite("Data Source=InnkeepServer.db");
		return new InnkeepServerContext(optionsBuilder.Options);
	}
	
	public DbSet<Transaction> Transactions { get; set; }
	
	public DbSet<Authentication> Authentications { get; set; }
}