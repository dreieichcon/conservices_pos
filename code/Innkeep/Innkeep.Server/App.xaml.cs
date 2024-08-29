using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Innkeep.Db.Server.Context;
using Innkeep.Strings;
using Innkeep.Server.Startup;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;

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
		
		LoggingManager.InitializeLogger("Innkeep Server");

		var host = KestrelBuilder.Build();

		DatabaseCreator.EnsureDbCreated(
			host.Services.GetRequiredService<IDbContextFactory<InnkeepServerContext>>()
			);
		
		Task.Run(() => ServerServiceInitializer.InitializeServices(host.Services));

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}
}