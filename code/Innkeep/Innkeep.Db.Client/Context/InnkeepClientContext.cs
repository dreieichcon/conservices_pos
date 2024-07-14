using Innkeep.Db.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Client.Context;

public class InnkeepClientContext(DbContextOptions<InnkeepClientContext> options) : DbContext(options)
{
	public DbSet<ClientConfig> ClientConfigs { get; init; } = null!;
}