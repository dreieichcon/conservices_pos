using System.Windows;
using Innkeep.Client.Startup;
using Innkeep.Db.Client.Context;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Innkeep.Strings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Velopack;

namespace Innkeep.Client;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		ThreadCultureHelper.SetInvariant();
		LoggingManager.InitializeLogger("Innkeep Client");

		base.OnStartup(e);

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

		DatabaseCreator.EnsureDbCreated(host.Services.GetRequiredService<IDbContextFactory<InnkeepClientContext>>());

		ClientEventInitializer.Initialize(host.Services);

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}

	private static async Task UpdateApp()
	{
		var mgr = new UpdateManager("https://updates.conservices.de/innkeep/client");

		var newVersion = await mgr.CheckForUpdatesAsync();

		if (newVersion == null)
			return;

		await mgr.DownloadUpdatesAsync(newVersion);

		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}