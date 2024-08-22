using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Innkeep.Client.Startup;

public static class WpfBuilder
{
	public static void ConfigureServices(IServiceCollection collection)
	{
		collection.AddWpfBlazorWebView();
		collection.AddBlazorWebViewDeveloperTools();
		collection.AddMudServices(
			options =>
			{
				options.SnackbarConfiguration.VisibleStateDuration = 2000;
			});
	}
}