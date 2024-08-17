using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Innkeep.Client.Startup;
using Innkeep.Db.Client.Context;
using Innkeep.Resources;
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
		ThreadCultureHelper.SetInvariant();

		base.OnStartup(e);

		ServicePointManager.ServerCertificateValidationCallback = LocalHostTesting;
		LoggingManager.InitializeLogger("Innkeep Client");

		var host = KestrelBuilder.Build();

		DatabaseCreator.EnsureDbCreated(host.Services.GetRequiredService<IDbContextFactory<InnkeepClientContext>>());

		ClientEventInitializer.Initialize(host.Services);

		var mainWindow = new MainWindow(host);
		mainWindow.Show();
	}

	private static bool LocalHostTesting(
		object sender,
		X509Certificate? certificate,
		X509Chain? chain,
		SslPolicyErrors sslPolicyErrors
	)
	{
		return true;
	}
}