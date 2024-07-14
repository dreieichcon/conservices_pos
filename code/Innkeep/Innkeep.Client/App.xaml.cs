using System.Configuration;
using System.Data;
using System.Windows;
using Innkeep.Client.Db.Context;
using Innkeep.Client.Startup;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Client;

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
			host.Services.GetRequiredService<IDbContextFactory<InnkeepClientContext>>()
			);

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}
}