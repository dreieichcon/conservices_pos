using System.Windows;
using Innkeep.Client.Startup;
using Innkeep.Startup.Services;
using Innkeep.Strings;

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

		Bootstrapper.Run();
	}
}