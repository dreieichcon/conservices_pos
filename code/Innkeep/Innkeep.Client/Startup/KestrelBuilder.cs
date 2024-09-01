using System.Security.Cryptography.X509Certificates;
using Innkeep.Client.Services;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Client.Interfaces.Internal;
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
				services.AddSingleton<IClientScreenService, ClientScreenService>();
				ClientServiceManager.ConfigureServices(services, true);
				WpfBuilder.ConfigureServices(services);

				services.AddEndpointsApiExplorer();
				services.AddSwaggerGen();
			}
		);
		
		var certificate = new X509Certificate2("./cert/cert.pfx");

		builder.ConfigureWebHostDefaults(
			whb =>
			{
				whb.ConfigureKestrel(options =>
				{
					options.ListenAnyIP(42069, configure =>
					{
						configure.UseHttps(
							httpsOptions =>
							{
								httpsOptions.ServerCertificate = certificate;
								httpsOptions.AllowAnyClientCertificate();
							});
					});
				});
				whb.UseStartup<KestrelStartup>();
			}
		);

		return builder.Build();
	}
}