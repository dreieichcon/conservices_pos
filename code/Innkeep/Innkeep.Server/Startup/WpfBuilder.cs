using Innkeep.Startup.Services;
using MudBlazor.Services;

namespace Innkeep.Server.Startup;

public static class WpfBuilder
{
	public static IServiceProvider Build()
	{
		var collection = new ServiceCollection();
		
		collection.AddWpfBlazorWebView();
		collection.AddBlazorWebViewDeveloperTools();
		collection.AddMudServices();
		
		ServerServiceManager.ConfigureServices(collection);

		return collection.BuildServiceProvider();
	}
}