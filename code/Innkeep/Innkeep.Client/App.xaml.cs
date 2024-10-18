using System.Windows;
using Innkeep.Client.Startup;
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

		base.OnStartup(e);

		Bootstrapper.Run();
	}
}