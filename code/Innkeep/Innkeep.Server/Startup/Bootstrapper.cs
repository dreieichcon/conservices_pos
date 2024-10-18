using System.IO;
using System.Windows;
using Innkeep.Core.Constants;
using Innkeep.Core.Env;
using Innkeep.Db.Server.Context;
using Innkeep.Services.Interfaces.Db;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;
using Velopack;
#if RELEASE
using Innkeep.Updates;
#endif

namespace Innkeep.Server.Startup;

public static partial class Bootstrapper
{
	public static void Run()
	{
		LoadEnvironmentVariables();

		// Setup Serilog for the Client
		LoggingManager.InitializeLogger("Innkeep Server", ServerPaths.LogFilePath);

		// Setup Velopack and Check for Updates
		ConfigureUpdates();

		// In case Velopack wants to quit the app to update
		// abort the current startup
		if (Environment.HasShutdownStarted)
			return;

		// Create the main application host
		var host = CreateHost();

		// Initialize the db and run migrations 
		DatabaseCreator.EnsureDbCreated(host.Services.GetRequiredService<IDbContextFactory<InnkeepServerContext>>());

		// Initialize service instances
		Task
			.Run(async () => InitializeServices(host))
			.GetAwaiter()
			.GetResult();

		// Start application
		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}

	/// <summary>
	///     Enables Velopack and checks for updates.
	/// </summary>
	private static void ConfigureUpdates()
	{
		VelopackApp
			.Build()
			.Run();

# if RELEASE
		Task.Run(async () => await InnkeepUpdater.CheckForUpdates("https://updates.conservices.de/innkeep/server"))
			.GetAwaiter()
			.GetResult();
#endif
	}

	/// <summary>
	///     Initializes services by instantiating them and performs other initialization functions.
	/// </summary>
	/// <param name="host">Current host, holding the services.</param>
	private static async Task InitializeServices(IHost host)
	{
		var salesItemService = host.Services.GetRequiredService<IPretixSalesItemService>();

		await host
			.Services.GetRequiredService<IRegisterService>()
			.Load();

		Task.Run(async () => await salesItemService.ReloadTimer());

		var transactionSettingsService = host.Services.GetRequiredService<ITransactionDbSettingsService>();
		transactionSettingsService.LoadSettings();

		if (transactionSettingsService.DbExists)
		{
			var factory = host.Services.GetRequiredService<IDbContextFactory<InnkeepTransactionContext>>();
			DatabaseCreator.EnsureDbCreated(factory);
		}
	}

	private static void LoadEnvironmentVariables()
	{
		if (!Directory.Exists(ServerPaths.DataDirectory))
			Directory.CreateDirectory(ServerPaths.DataDirectory);

		if (!Directory.Exists(ServerPaths.EnvDirectory))
			Directory.CreateDirectory(ServerPaths.EnvDirectory);

		// load environment variables
		var result = Env.Load(ServerPaths.EnvFilePath);

		if (!result)
			MessageBox.Show(
				"Error while loading the environment variables. Logging to discord will be disabled.",
				"Error",
				MessageBoxButton.OK,
				MessageBoxImage.Error
			);
	}
}