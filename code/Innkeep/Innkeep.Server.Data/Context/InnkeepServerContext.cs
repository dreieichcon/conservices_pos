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
	}

	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<Authentication> Authentications { get; set; }
	
	public DbSet<Organizer> Organizers { get; set; }
	
	public DbSet<Event> Events { get; set; }
	
	public DbSet<Register> Registers { get; set; }

	public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
	
	
}