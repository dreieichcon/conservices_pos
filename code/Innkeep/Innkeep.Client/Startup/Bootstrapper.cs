using Innkeep.Db.Client.Context;
using Innkeep.Services.Client.Interfaces.Pos;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Velopack;

#if RELEASE
using Innkeep.Updates;
#endif

namespace Innkeep.Client.Startup;

public static partial class Bootstrapper
{
	public static void Run()
	{
		// Setup Serilog for the Client
		LoggingManager.InitializeLogger("Innkeep Client");

		// Setup Velopack and Check for Updates
		ConfigureUpdates();

		// In case Velopack wants to quit the app to update
		// abort the current startup
		if (Environment.HasShutdownStarted)
			return;

		// Create the main application host
		var host = CreateHost();

		// Initialize the db and run migrations 
		DatabaseCreator.EnsureDbCreated(host.Services.GetRequiredService<IDbContextFactory<InnkeepClientContext>>());

		// Initialize service instances
		InitializeServices(host);

		// Start application
		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}

	/// <summary>
	///     Enables Velopack and checks for updates.
	/// </summary>
	private static void ConfigureUpdates()
	{
		VelopackApp.Build()
					.Run();

# if RELEASE
		Task.Run(async () => await InnkeepUpdater.CheckForUpdates("https://updates.conservices.de/innkeep/client"))
			.GetAwaiter()
			.GetResult();
#endif
	}

	/// <summary>
	///     Initializes services by instantiating them and performs other initialization functions.
	/// </summary>
	/// <param name="host">Current host, holding the services.</param>
	private static void InitializeServices(IHost host)
	{
		host.Services.GetRequiredService<ISalesItemService>();
	}
}