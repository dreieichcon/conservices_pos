using Innkeep.Core.Interfaces;
using Innkeep.Client.Interfaces.Services;
using Innkeep.DI.Services;
using Innkeep.DI.Services.Client.Core;
using Innkeep.DI.Services.Client.Hardware;
using Innkeep.DI.Services.Server;
using Innkeep.Server.Api.Pretix;
using Innkeep.Server.Api.Register;
using Innkeep.Server.Api.Transaction;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Interfaces.Fiskaly;
using Innkeep.Server.Data.Repositories;
using Innkeep.Server.Data.Repositories.Fiskaly;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Server.Pretix.Interfaces;
using Innkeep.Server.Pretix.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ITransactionService = Innkeep.Client.Interfaces.Services.ITransactionService;

namespace Innkeep.DI;

public static class DependencyManager
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    public static void InitializeTests()
    {
        ServiceProvider = RegisterTests();
    }

    private static IServiceProvider RegisterTests()
    {
        var serviceCollection = new ServiceCollection();
        
        ConfigureServices(serviceCollection);
        
        return Create(serviceCollection);
    }
    
    private static void ConfigureServices(IServiceCollection collection)
    {
        collection.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
        collection.AddSingleton<IAuthenticationService, AuthenticationService>();
        
        collection.AddSingleton<IPretixRepository, PretixRepository>();
        collection.AddSingleton<IPretixService, PretixService>();

        collection.AddSingleton<IPopupService, PopupService>();

        collection.AddSingleton<IShoppingCartService, ShoppingCartService>();
    }

    private static IServiceProvider Create(IServiceCollection serviceCollection)
    {
        return serviceCollection.BuildServiceProvider();
    }
    
    public static void InitializeClient(WebApplicationBuilder builder)
    {
        ConfigureClientServices(builder.Services);
    }

    public static void InitializeServer(WebApplicationBuilder builder)
    {
        ConfigureServerServices(builder.Services);
    }

    private static void ConfigureServerServices(IServiceCollection collection)
    {
        collection.AddDbContext<InnkeepServerContext>((_, builder) => builder.UseSqlite("Data Source=InnkeepServer.db"));
        
        collection.AddSingleton<ITransactionRepository, TransactionRepository>();
        collection.AddSingleton<IFiskalyApiSettingsRepository, FiskalyApiSettingsRepository>();
        collection.AddSingleton<IFiskalyApiSettingsService, FiskalyApiSettingsService>();
        
        collection.AddSingleton<ITseService, TseService>();
        collection.AddSingleton<ICashFlowRepository, CashFlowRepository>();
        collection.AddSingleton<ICashFlowService, CashFlowService>();

        collection.AddScoped<RegisterDetectionController>();
        collection.AddScoped<PretixRequestController>();

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
        collection.AddScoped<TransactionRequestController>();
    }

    private static void ConfigureClientServices(IServiceCollection collection)
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

        collection.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        collection.AddSingleton<IAuthenticationService, AuthenticationService>();

        collection.AddSingleton<IPopupService, PopupService>();

        collection.AddSingleton<IShoppingCartService, ShoppingCartService>();
        collection.AddSingleton<IAmountKeypadService, AmountKeypadService>();

        collection.AddSingleton<ITransactionService, TransactionService>();
    }
}