using Innkeep.Server.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Db.Context;

public class InnkeepServerContext : DbContext
{
	public InnkeepServerContext(DbContextOptions<InnkeepServerContext> options) : base(options)
	{
		
	}
	
	public DbSet<PretixConfig> PretixConfigs { get; set; } = null!;

	public DbSet<FiskalyConfig> FiskalyConfigs { get; set; } = null!;

	public DbSet<FiskalyTseConfig> TseConfigs { get; set; } = null!;
}