using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.Core.Interfaces.Repositories;
using Innkeep.Core.Interfaces.Services;
using Innkeep.Client.Data.Repositories.FileOperations;
using Innkeep.Client.Data.Repositories.Pretix;
using Innkeep.DI.Services;
using Microsoft.AspNetCore.Builder;
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
        RegisterClient(builder);
    }

    private static void RegisterClient(WebApplicationBuilder builder)
    {
        ConfigureClientServices(builder.Services);
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