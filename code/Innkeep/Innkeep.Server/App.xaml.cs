using System.Windows;
using Innkeep.Db.Server.Context;
using Innkeep.Server.Startup;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Innkeep.Strings;
using Microsoft.EntityFrameworkCore;
using Velopack;

namespace Innkeep.Server;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		ThreadCultureHelper.SetInvariant();
		base.OnStartup(e);

		LoggingManager.InitializeLogger("Innkeep Server");

		VelopackApp.Build().Run();

# if RELEASE
		try
		{
			Task.Run(async () => await UpdateApp()).GetAwaiter().GetResult();

			if (Environment.HasShutdownStarted)
				return;
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Error while checking for updates");
		}
# endif

		var host = KestrelBuilder.Build();

		DatabaseCreator.EnsureDbCreated(host.Services.GetRequiredService<IDbContextFactory<InnkeepServerContext>>());

		Task.Run(() => ServerServiceInitializer.InitializeServices(host.Services));

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}

	private static async Task UpdateApp()
	{
		var mgr = new UpdateManager("https://updates.conservices.de/innkeep/server");

		var newVersion = await mgr.CheckForUpdatesAsync();

		if (newVersion == null)
			return;

		await mgr.DownloadUpdatesAsync(newVersion);

		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}