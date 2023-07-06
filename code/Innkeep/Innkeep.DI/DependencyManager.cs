using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Services;
using Innkeep.Client.Interfaces.Services;
using Innkeep.DI.Services;
using Innkeep.Server.Api.Register;
using Innkeep.Server.Api.Transaction;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Repositories;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Server.Pretix.Interfaces;
using Innkeep.Server.Pretix.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        collection.AddDbContext<InnkeepServerContext>((provider, builder) => builder.UseSqlite("Data Source=InnkeepServer.db"));
        
        collection.AddScoped<RegisterDetectionController>();
        collection.AddScoped<TransactionRequestController>();

        collection.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>();

        collection.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        collection.AddSingleton<IAuthenticationService, AuthenticationService>();
        collection.AddSingleton<IPretixRepository, PretixRepository>();
        collection.AddSingleton<IPretixService, PretixService>();
    }

    private static void ConfigureClientServices(IServiceCollection collection)
    {
        collection.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        collection.AddSingleton<IAuthenticationService, AuthenticationService>();
        
        collection.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>();
        
        collection.AddSingleton<IPretixRepository, PretixRepository>();
        collection.AddSingleton<IPretixService, PretixService>();

        collection.AddSingleton<IPopupService, PopupService>();

        collection.AddSingleton<IShoppingCartService, ShoppingCartService>();
        
        collection.AddSingleton<IAmountKeypadService, AmountKeypadService>();

        collection.AddSingleton<ITransactionService, TransactionService>();
    }
}