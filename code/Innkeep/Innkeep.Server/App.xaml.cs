using System.Windows;
using Innkeep.Db.Server.Context;
using Innkeep.Resources;
using Innkeep.Server.Startup;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Innkeep.Server;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		ThreadCultureHelper.SetInvariant();
		base.OnStartup(e);

		LoggingManager.InitializeLogger();

		var host = KestrelBuilder.Build();

		DatabaseCreator.EnsureDbCreated(
			host.Services.GetRequiredService<IDbContextFactory<InnkeepServerContext>>()
			);
		
		Task.Run(() => ServerServiceInitializer.InitializeServices(host.Services));

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}
}