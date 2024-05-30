using Innkeep.Server.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Startup.Services;

public static class ServerServiceManager
{
	public static void ConfigureServices(IServiceCollection collection)
	{
		ConfigureDatabase(collection);
	}

	private static void ConfigureDatabase(IServiceCollection collection)
	{
		if (!Directory.Exists("./db")) 
			Directory.CreateDirectory("./db");
		
		collection.AddDbContextFactory<InnkeepServerContext>(options => options.UseSqlite("DataSource=./db/server.db"));
		collection.AddDbContext<InnkeepServerContext>();
	}
}