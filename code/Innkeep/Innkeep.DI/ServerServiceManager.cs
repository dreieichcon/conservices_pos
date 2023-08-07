using Innkeep.Api.Fiskaly.Interfaces;
using Innkeep.Api.Fiskaly.Repositories;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Interfaces.Fiskaly;
using Innkeep.Server.Data.Interfaces.Pretix;
using Innkeep.Server.Data.Interfaces.Register;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Data.Repositories.ApplicationSettings;
using Innkeep.Server.Data.Repositories.Fiskaly;
using Innkeep.Server.Data.Repositories.Pretix;
using Innkeep.Server.Data.Repositories.Register;
using Innkeep.Server.Data.Repositories.Transactions;
using Innkeep.Server.Services.Interfaces.Api;
using Innkeep.Server.Services.Interfaces.Db;
using Innkeep.Server.Services.Services.Api;
using Innkeep.Server.Services.Services.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.DI;

public static class ServerServiceManager
{
	public static void Initialize(WebApplicationBuilder builder)
	{
		ConfigureServerServices(builder.Services);
	}
	
	public static void ConfigureServerServices(IServiceCollection collection)
	{
		
		#if RELEASE
		using var context = InnkeepServerContext.Create(true)
		#endif
		
		collection.AddDbContext<InnkeepServerContext>((_, builder) => builder.UseSqlite("Data Source=InnkeepServer.db"));
        
		collection.AddSingleton<ITransactionRepository, TransactionRepository>();
		
		collection.AddSingleton<IFiskalyApiSettingsRepository, FiskalyApiSettingsRepository>();
		collection.AddSingleton<IFiskalyApiSettingsService, FiskalyApiSettingsService>();

		collection.AddSingleton<IFiskalyAuthenticationRepository, FiskalyAuthenticationRepository>();
		collection.AddSingleton<IFiskalyTransactionRepository, FiskalyTransactionRepository>();

		collection.AddSingleton<IFiskalyService, FiskalyService>();
		
		collection.AddSingleton<ITseService, TseService>();
		collection.AddSingleton<ICashFlowRepository, CashFlowRepository>();
		collection.AddSingleton<ICashFlowService, CashFlowService>();

		collection.AddSingleton<IRegisterRepository, RegisterRepository>();
		collection.AddSingleton<IRegisterService, RegisterService>();

		collection.AddSingleton<IOrganizerRepository, OrganizerRepository>();
		collection.AddSingleton<IEventRepository, EventRepository>();

		collection.AddSingleton<IApplicationSettingsRepository, ApplicationSettingsRepository>();
		collection.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>();

		collection.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
		collection.AddSingleton<IAuthenticationService, AuthenticationService>();
        
		collection.AddSingleton<IPretixRepository, PretixRepository>();
		collection.AddSingleton<IPretixService, PretixService>();
        
		collection.AddSingleton<IServerTransactionService, ServerTransactionService>();
		
	}
}