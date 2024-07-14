using Innkeep.Db.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Context;

public class InnkeepServerContext(DbContextOptions<InnkeepServerContext> options) : DbContext(options)
{
	public DbSet<PretixConfig> PretixConfigs { get; init; } = null!;

	public DbSet<FiskalyConfig> FiskalyConfigs { get; init; } = null!;

	public DbSet<FiskalyTseConfig> TseConfigs { get; init; } = null!;

	public DbSet<Register> Registers { get; init; } = null!;
}