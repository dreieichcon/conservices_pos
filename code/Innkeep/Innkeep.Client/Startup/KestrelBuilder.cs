using Innkeep.Client.Services;
using Innkeep.Client.Services.Interfaces.Hardware;
using Innkeep.Startup.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Innkeep.Client.Startup;

public class KestrelBuilder
{
	public static IHost Build()
	{
		var builder = Host.CreateDefaultBuilder();

		builder.ConfigureServices(
			services =>
			{
				services.AddSingleton<IPrinterService, PrinterService>();
				ClientServiceManager.ConfigureServices(services, true);
				WpfBuilder.ConfigureServices(services);

				services.AddEndpointsApiExplorer();
				services.AddSwaggerGen();
			}
		);

		builder.ConfigureWebHostDefaults(
			whb =>
			{
				whb.ConfigureKestrel(options => options.ListenAnyIP(42069, configure => configure.UseHttps()));
				whb.UseStartup<KestrelStartup>();
			}
		);

		return builder.Build();
	}
}