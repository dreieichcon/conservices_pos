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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Event>().Navigation(x => x.Organizer).AutoInclude();

		modelBuilder.Entity<ApplicationSetting>().Navigation(x => x.SelectedOrganizer).AutoInclude();
		modelBuilder.Entity<ApplicationSetting>().Navigation(x => x.SelectedEvent).AutoInclude();

		modelBuilder.Entity<Transaction>().Navigation(x => x.Event).AutoInclude();
		modelBuilder.Entity<Transaction>().Navigation(x => x.Organizer).AutoInclude();
		modelBuilder.Entity<Transaction>().Navigation(x => x.Device).AutoInclude();

		modelBuilder.Entity<CashFlow>().Navigation(x => x.Event).AutoInclude();
		modelBuilder.Entity<CashFlow>().Navigation(x => x.Register).AutoInclude();
	}

	public DbSet<Transaction> Transactions { get; set; } = null!;
	public DbSet<Authentication> Authentications { get; set; } = null!;
	
	public DbSet<Organizer> Organizers { get; set; } = null!;
	
	public DbSet<Event> Events { get; set; } = null!;
	
	public DbSet<Register> Registers { get; set; } = null!;

	public DbSet<ApplicationSetting> ApplicationSettings { get; set; } = null!;
	
	public DbSet<CashFlow> CashFlows { get; set; } = null!;

	public DbSet<FiskalyApiSettings> FiskalySettings { get; set; } = null!;
}