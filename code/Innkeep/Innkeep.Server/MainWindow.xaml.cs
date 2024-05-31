using System.Windows;

namespace Innkeep.Server;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow(IServiceProvider provider)
	{
		InitializeComponent();

		Resources.Add("services", provider);
	}
}