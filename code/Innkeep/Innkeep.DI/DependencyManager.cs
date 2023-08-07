using Innkeep.Server.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Innkeep.DI;

public static class DependencyManager
{
	public static IServiceProvider ServiceProvider { get; private set; } = null!;

	public static void InitializeServer(WebApplicationBuilder builder)
	{
		ServerServiceManager.Initialize(builder);
		ServerControllerManager.Initialize(builder);
	}

	public static void InitializeClient(WebApplicationBuilder builder)
	{
		ClientServiceManager.Initialize(builder);
	}

	public static void InitializeTests()
	{
		Log.Logger = new LoggerConfiguration()
					.WriteTo.Console()
					.WriteTo.Debug()
					.WriteTo.Trace()
					.MinimumLevel.Verbose()
					.CreateLogger();
		
		var serviceCollection = new ServiceCollection();

		ServerServiceManager.ConfigureServerServices(serviceCollection);
		ClientServiceManager.ConfigureClientServices(serviceCollection);

		ServiceProvider = serviceCollection.BuildServiceProvider();
	}
}