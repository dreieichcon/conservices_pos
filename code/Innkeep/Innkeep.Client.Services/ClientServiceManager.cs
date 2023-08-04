using Innkeep.Api.Client.Interfaces;
using Innkeep.Api.Client.Repositories;
using Innkeep.Client.Interfaces.Services;
using Innkeep.Client.Services.Interfaces.File;
using Innkeep.Client.Services.Interfaces.Hardware;
using Innkeep.Client.Services.Interfaces.Pretix;
using Innkeep.Client.Services.Interfaces.Server;
using Innkeep.Client.Services.Interfaces.Transaction;
using Innkeep.Client.Services.Interfaces.Ui;
using Innkeep.Client.Services.Services.File;
using Innkeep.Client.Services.Services.Hardware;
using Innkeep.Client.Services.Services.Server;
using Innkeep.Client.Services.Services.Transaction;
using Innkeep.Client.Services.Services.Ui;
using Innkeep.DI.Services;
using Innkeep.DI.Services.Client.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Client.Services;

public static class ClientServiceManager
{
	public static void Initialize(WebApplicationBuilder builder)
	{
		ConfigureClientServices(builder.Services);
	}
	
	public static void ConfigureClientServices(IServiceCollection collection)
	{
		collection.AddSingleton<INetworkHardwareService, NetworkHardwareService>();
		collection.AddSingleton<IClientSettingsRepository, ClientSettingsRepository>();
		collection.AddSingleton<IClientSettingsService, ClientSettingsService>();

		collection.AddSingleton<IClientServerConnectionRepository, ClientServerConnectionRepository>();
		collection.AddSingleton<IClientServerConnectionService, ClientServerConnectionService>();
        
		collection.AddSingleton<ISerialPortRepository, SerialPortRepository>();
		collection.AddSingleton<IPrintService, PrintService>();

		collection.AddSingleton<IClientPretixRepository, ClientPretixRepository>();
		collection.AddSingleton<IClientPretixService, ClientPretixService>();

		collection.AddSingleton<IPopupService, PopupService>();

		collection.AddSingleton<IShoppingCartService, ShoppingCartService>();
		collection.AddSingleton<IAmountKeypadService, AmountKeypadService>();

		collection.AddSingleton<ITransactionService, TransactionService>();
	}
}