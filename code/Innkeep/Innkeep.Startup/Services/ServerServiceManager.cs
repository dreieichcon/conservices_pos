using Demolite.Db.Interfaces;
using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Transaction;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Auth;
using Innkeep.Api.Fiskaly.Repositories.Transaction;
using Innkeep.Api.Fiskaly.Repositories.Tss;
using Innkeep.Api.Internal.Interfaces.Client.Actions;
using Innkeep.Api.Internal.Interfaces.Client.Printing;
using Innkeep.Api.Internal.Repositories.Client.Actions;
using Innkeep.Api.Internal.Repositories.Client.Printing;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Interfaces.Auth;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Api.Pretix.Interfaces.Quota;
using Innkeep.Api.Pretix.Interfaces.Sales;
using Innkeep.Api.Pretix.Repositories.Auth;
using Innkeep.Api.Pretix.Repositories.Checkin;
using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Repositories.Order;
using Innkeep.Api.Pretix.Repositories.Quota;
using Innkeep.Api.Pretix.Repositories.Sales;
using Innkeep.Db.Server.Context;
using Innkeep.Db.Server.Models.Config;
using Innkeep.Db.Server.Models.Transaction;
using Innkeep.Db.Server.Repositories.Config;
using Innkeep.Db.Server.Repositories.Registers;
using Innkeep.Db.Server.Repositories.Transaction;
using Innkeep.Server.Controllers.Endpoints;
using Innkeep.Services.Hardware;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Db;
using Innkeep.Services.Interfaces.Hardware;
using Innkeep.Services.Interfaces.Internal;
using Innkeep.Services.Server.Authentication;
using Innkeep.Services.Server.Database;
using Innkeep.Services.Server.Fiskaly.Client;
using Innkeep.Services.Server.Fiskaly.Tss;
using Innkeep.Services.Server.Interfaces.Fiskaly.Client;
using Innkeep.Services.Server.Interfaces.Fiskaly.Transaction;
using Innkeep.Services.Server.Interfaces.Fiskaly.Tss;
using Innkeep.Services.Server.Interfaces.Internal;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Services.Server.Interfaces.Transaction;
using Innkeep.Services.Server.Internal;
using Innkeep.Services.Server.Pretix;
using Innkeep.Services.Server.Registers;
using Innkeep.Services.Server.Transaction;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FiskalyTransactionService = Innkeep.Services.Server.Fiskaly.Transaction.FiskalyTransactionService;

namespace Innkeep.Startup.Services;

public static class ServerServiceManager
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

		collection.AddSingleton<IEventStateService, EventStateService>();
		collection.AddSingleton<IStartupService, StartupService>();
	}

	private static void ConfigureLocalServices(IServiceCollection collection)
	{
		collection.AddSingleton<IHardwareService, HardwareService>();
		collection.AddSingleton<ITransactionDbSettingsService, TransactionDbSettingsService>();
	}

	private static void ConfigureControllers(IServiceCollection collection)
	{
		collection.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
		collection.AddControllers().AddApplicationPart(typeof(RegisterController).Assembly);
	}

	private static void ConfigureDatabase(IServiceCollection collection)
	{
		if (!Directory.Exists("./db"))
			Directory.CreateDirectory("./db");

		collection.AddDbContextFactory<InnkeepServerContext>(options => options.UseSqlite("DataSource=./db/server.db"));
		collection.AddDbContext<InnkeepServerContext>();
		collection.AddSingleton<IDbContextFactory<InnkeepTransactionContext>, InnkeepTransactionContextFactory>();
	}

	private static void ConfigureDbRepositories(IServiceCollection collection)
	{
		collection.AddSingleton<IDbRepository<PretixConfig>, PretixConfigRepository>();
		collection.AddSingleton<IDbRepository<FiskalyConfig>, FiskalyConfigRepository>();
		collection.AddSingleton<IDbRepository<FiskalyTseConfig>, FiskalyTseConfigRepository>();
		collection.AddSingleton<IDbRepository<Register>, RegisterRepository>();
		collection.AddSingleton<IDbRepository<TransactionModel>, TransactionRepository>();
	}

	private static void ConfigureDbServices(IServiceCollection collection)
	{
		collection.AddSingleton<IDbService<PretixConfig>, PretixConfigService>();
		collection.AddSingleton<IPretixAuthenticationService, PretixAuthenticationService>();

		collection.AddSingleton<IDbService<FiskalyConfig>, FiskalyConfigService>();
		collection.AddSingleton<IFiskalyAuthenticationService, FiskalyAuthenticationService>();
		collection.AddSingleton<IFiskalyTssService, FiskalyTssService>();

		collection.AddSingleton<ITransactionService, TransactionService>();
		collection.AddSingleton<IRegisterService, RegisterService>();
	}

	private static void ConfigureHttpRepositories(IServiceCollection collection)
	{
		collection.AddSingleton<IPretixAuthenticationRepository, PretixAuthenticationRepository>();
		collection.AddSingleton<IPretixOrganizerRepository, PretixOrganizerRepository>();
		collection.AddSingleton<IPretixEventRepository, PretixEventRepository>();
		collection.AddSingleton<IPretixCheckinListRepository, PretixCheckinListListRepository>();
		collection.AddSingleton<IPretixCheckinRepository, PretixCheckinRepository>();
		collection.AddSingleton<IPretixSalesItemRepository, PretixSalesItemRepository>();
		collection.AddSingleton<IPretixOrderRepository, PretixOrderRepository>();
		collection.AddSingleton<IPretixQuotaRepository, PretixQuotaRepository>();

		collection.AddSingleton<IFiskalyAuthenticationRepository, FiskalyAuthenticationRepository>();
		collection.AddSingleton<IFiskalyTssRepository, FiskalyTssRepository>();
		collection.AddSingleton<IFiskalyClientRepository, FiskalyClientRepository>();
		collection.AddSingleton<IFiskalyTransactionRepository, FiskalyTransactionRepository>();

		collection.AddSingleton<IClientPrintRepository, ClientPrintRepository>();
		collection.AddSingleton<IClientReloadRepository, ClientReloadRepository>();
	}

	private static void ConfigureHttpServices(IServiceCollection collection)
	{
		collection.AddSingleton<IPretixSalesItemService, PretixSalesItemService>();
		collection.AddSingleton<IPretixCheckinService, PretixCheckinService>();
		collection.AddSingleton<IPretixOrderService, PretixOrderService>();

		collection.AddSingleton<IFiskalyTssService, FiskalyTssService>();
		collection.AddSingleton<IFiskalyClientService, FiskalyClientService>();
		collection.AddSingleton<IFiskalyTransactionService, FiskalyTransactionService>();
	}
}