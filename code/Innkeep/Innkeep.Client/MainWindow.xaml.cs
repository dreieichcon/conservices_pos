using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Innkeep.Client;

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