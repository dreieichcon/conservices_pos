using Innkeep.Startup.Services;
using MudBlazor.Services;

namespace Innkeep.Server.Startup;

public static class KestrelBuilder
{
	public static WebApplication Build()
	{
		var builder = WebApplication.CreateBuilder();

		builder.Services.AddEndpointsApiExplorer();
		
		ServerServiceManager.ConfigureServices(builder.Services, true);
		
		builder.WebHost.ConfigureKestrel(
			options => options.ListenAnyIP(1337, configure => configure.UseHttps()));

		var app = builder.Build();

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		
		app.MapControllers();

		return app;
	}
}