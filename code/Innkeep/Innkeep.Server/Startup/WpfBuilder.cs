using MudBlazor.Services;

namespace Innkeep.Server.Startup;

public static class WpfBuilder
{
	public static void ConfigureServices(IServiceCollection collection)
	{
		collection.AddWpfBlazorWebView();
		collection.AddBlazorWebViewDeveloperTools();
		collection.AddMudServices();
	}
}