using Innkeep.Api.Server.Interfaces;
using Innkeep.Api.Server.Repositories.Pos;
using Innkeep.Api.Server.Repositories.Registers;
using Innkeep.Client.Controllers.Endpoints;
using Innkeep.Db.Client.Context;
using Innkeep.Db.Client.Models;
using Innkeep.Db.Client.Repositories.Config;
using Innkeep.Db.Interfaces;
using Innkeep.Services.Client.Database;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Services.Client.Interfaces.Pos;
using Innkeep.Services.Client.Interfaces.Registers;
using Innkeep.Services.Client.Internal;
using Innkeep.Services.Client.Pos;
using Innkeep.Services.Client.Registers;
using Innkeep.Services.Hardware;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Startup.Services;

public static class ClientServiceManager
{
	public static void ConfigureServices(IServiceCollection collection, bool isKestrel = false)
	{
		ConfigureLocalServices(collection);
		ConfigureDatabase(collection);
		ConfigureDbRepositories(collection);
		ConfigureDbServices(collection);
		ConfigureHttpRepositories(collection);
		ConfigureHttpServices(collection);
		
		if (isKestrel)
			ConfigureControllers(collection);
		
		collection.AddSingleton<IStartupService, StartupService>();
	}

	private static void ConfigureLocalServices(IServiceCollection collection)
	{
		collection.AddSingleton<IEventRouter, EventRouter>();
		collection.AddSingleton<IHardwareService, HardwareService>();
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
	
	private static void ConfigureDbRepositories(IServiceCollection collection)
	{
		collection.AddSingleton<IDbRepository<ClientConfig>, ClientConfigRepository>();
	}

	private static void ConfigureDbServices(IServiceCollection collection)
	{
		collection.AddSingleton<IDbService<ClientConfig>, ClientConfigService>();
	}

	private static void ConfigureHttpRepositories(IServiceCollection collection)
	{
		collection.AddSingleton<IRegisterConnectionRepository, RegisterConnectionRepository>();
		collection.AddSingleton<ISalesItemRepository, SalesItemRepository>();
		collection.AddSingleton<ITransactionRepository, TransactionRepository>();
	}

	private static void ConfigureHttpServices(IServiceCollection collection)
	{
		collection.AddSingleton<IRegisterConnectionService, RegisterConnectionService>();
		collection.AddSingleton<ISalesItemService, SalesItemService>();
		collection.AddSingleton<ITransactionService, TransactionService>();
	}
}