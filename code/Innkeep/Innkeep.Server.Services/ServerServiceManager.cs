using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Interfaces.Fiskaly;
using Innkeep.Server.Data.Repositories;
using Innkeep.Server.Data.Repositories.Fiskaly;
using Innkeep.Server.Services.Interfaces;
using Innkeep.Server.Services.Interfaces.Db;
using Innkeep.Server.Services.Services;
using Innkeep.Server.Services.Services.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Server.Services;

public class ServerServiceManager
{
	public static void Initialize(WebApplicationBuilder builder)
	{
		ConfigureServerServices(builder.Services);
	}
	
	public static void ConfigureServerServices(IServiceCollection collection)
	{
		collection.AddDbContext<InnkeepServerContext>((_, builder) => builder.UseSqlite("Data Source=InnkeepServer.db"));
        
		collection.AddSingleton<ITransactionRepository, TransactionRepository>();
		collection.AddSingleton<IFiskalyApiSettingsRepository, FiskalyApiSettingsRepository>();
		collection.AddSingleton<IFiskalyApiSettingsService, FiskalyApiSettingsService>();
        
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