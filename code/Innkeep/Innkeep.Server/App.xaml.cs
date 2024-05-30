using System.Windows;
using Innkeep.Server.Db.Context;
using Innkeep.Startup.Database;
using Innkeep.Startup.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Innkeep.Server;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		
		var collection = new ServiceCollection();
		collection.AddWpfBlazorWebView();
		collection.AddBlazorWebViewDeveloperTools();
		collection.AddMudServices();
        
		ServerServiceManager.ConfigureServices(collection);

		var provider = collection.BuildServiceProvider();
        
		DatabaseCreator.EnsureDbCreated(provider.GetRequiredService<IDbContextFactory<InnkeepServerContext>>());

		var mainWindow = new MainWindow(provider);
		mainWindow.Show();
	}
}