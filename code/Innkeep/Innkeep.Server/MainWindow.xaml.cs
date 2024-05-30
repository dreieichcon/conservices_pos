using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Innkeep.Server;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var collection = new ServiceCollection();
        collection.AddWpfBlazorWebView();
        collection.AddBlazorWebViewDeveloperTools();
        collection.AddMudServices();
        
        Resources.Add("services", collection.BuildServiceProvider());
    }
}