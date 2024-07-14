using Innkeep.Client.Controllers.Endpoints;
using Innkeep.Client.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Startup.Services;

public static class ClientServiceManager
{
	public static void ConfigureServices(IServiceCollection collection, bool isKestrel = false)
	{
		ConfigureDatabase(collection);
		
		if (isKestrel)
			ConfigureControllers(collection);
	}

	private static void ConfigureControllers(IServiceCollection collection)
	{
		collection.AddControllers().AddApplicationPart(typeof(ServerDataController).Assembly);
	}

	private static void ConfigureDatabase(IServiceCollection collection)
	{
		if (!Directory.Exists("./db")) 
			Directory.CreateDirectory("./db");
		
		collection.AddDbContextFactory<InnkeepClientContext>(options => options.UseSqlite("DataSource=./db/client.db"));
		collection.AddDbContext<InnkeepClientContext>();
	}
}