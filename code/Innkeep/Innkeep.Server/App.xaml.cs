using System.Windows;
using Innkeep.Server.Db.Context;
using Innkeep.Server.Startup;
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

		var app = KestrelBuilder.Build();
		var provider = WpfBuilder.Build();

		DatabaseCreator.EnsureDbCreated(provider.GetRequiredService<IDbContextFactory<InnkeepServerContext>>());
		
		_ = app.RunAsync();
		
		var mainWindow = new MainWindow(provider);
		mainWindow.Show();
	}
}