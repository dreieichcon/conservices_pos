using System.Security.Cryptography.X509Certificates;
using Innkeep.Startup.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using Serilog;

namespace Innkeep.Server.Startup;

public static class KestrelBuilder
{
	public static IHost Build()
	{
		var builder = Host.CreateDefaultBuilder();

		builder.ConfigureServices(
			services =>
			{
				ServerServiceManager.ConfigureServices(services, true);
				WpfBuilder.ConfigureServices(services);

				services.AddEndpointsApiExplorer();
				services.AddSwaggerGen();
			}
		);

		var certificate = new X509Certificate2("./cert/cert.pfx");

		builder.ConfigureWebHostDefaults(
			whb =>
			{
				whb.ConfigureKestrel(
					options =>
					{
						options.ListenAnyIP(
							1337,
							configure => configure.UseHttps(httpsOptions =>
							{
								httpsOptions.ServerCertificate = certificate;
								httpsOptions.AllowAnyClientCertificate();
							})
						);
					}
				);

				whb.UseStartup<KestrelStartup>();
			}
		);

		builder.UseSerilog();

		return builder.Build();
	}
}