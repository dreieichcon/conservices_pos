using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.Core.Interfaces.Repositories;
using Innkeep.Core.Interfaces.Services;
using Innkeep.Data.Repositories.FileOperations;
using Innkeep.Data.Repositories.Pretix;
using Innkeep.DI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.DI;

public static class DependencyManager
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    public static void Initialize()
    {
        ServiceProvider = Register();
    }

    private static IServiceProvider Register()
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

        collection.AddSingleton<IShoppingCartService, ShoppingCartService>();

    }

    private static IServiceProvider Create(IServiceCollection serviceCollection)
    {
        return serviceCollection.BuildServiceProvider();
    }
}