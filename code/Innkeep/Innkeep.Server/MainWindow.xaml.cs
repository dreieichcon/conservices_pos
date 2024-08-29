using System.ComponentModel;
using System.Windows;
using Serilog;

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

		Task.Run(async () =>
		{
			try
			{
				await _host.StartAsync();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host failed to start.");
			}
		});
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		_host.Dispose();
		base.OnClosing(e);
	}
}