using System.ComponentModel;
using System.Windows;

namespace Innkeep.Server;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly IHost _host;
	
	public MainWindow(IHost host)
	{
		_host = host;
		
		InitializeComponent();

		Resources.Add("services", _host.Services);

		_ = _host.StartAsync(CancellationToken.None);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		_host.Dispose();
		base.OnClosing(e);
	}
}