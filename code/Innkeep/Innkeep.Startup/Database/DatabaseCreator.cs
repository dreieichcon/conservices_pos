using Microsoft.EntityFrameworkCore;

namespace Innkeep.Startup.Database;

public class DatabaseCreator
{
	public static void EnsureDbCreated<T>(IDbContextFactory<T> factory) where T: DbContext
	{
		var db = factory.CreateDbContext();
		db.Database.Migrate();
	}
}