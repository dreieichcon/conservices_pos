using System.Windows;
using Innkeep.Server.Db.Context;
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
		base.OnStartup(e);

		LoggingManager.InitializeLogger();

		var host = KestrelBuilder.Build();

		DatabaseCreator.EnsureDbCreated(
			host.Services.GetRequiredService<IDbContextFactory<InnkeepServerContext>>()
			);

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}
}