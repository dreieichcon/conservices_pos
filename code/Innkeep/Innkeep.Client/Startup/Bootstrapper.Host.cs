using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Innkeep.Client.Services;
using Innkeep.Core.Constants;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Startup.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Serilog;

namespace Innkeep.Client.Startup;

public static partial class Bootstrapper
{
	/// <summary>
	///     Creates a new IHost instance and initializes the services.
	/// </summary>
	/// <returns></returns>
	private static IHost CreateHost()
	{
		var builder = Host.CreateDefaultBuilder();

		builder.ConfigureServices(
			services =>
			{
				// add net8-windows dependent services which are unavailable in the services project
				services.AddSingleton<IPrinterService, PrinterService>();
				services.AddSingleton<IClientScreenService, ClientScreenService>();

				// add all client services
				ClientServiceManager.ConfigureServices(services, true);

				// add Mudblazor and wpf services
				services.AddWpfBlazorWebView();
				services.AddBlazorWebViewDeveloperTools();

				services.AddMudServices(
					configuration => { configuration.SnackbarConfiguration.VisibleStateDuration = 2000; }
				);

				// add api services
				services.AddEndpointsApiExplorer();
				services.AddSwaggerGen();
			}
		);

		var certificate = ConfigureCertificate();

		builder.ConfigureWebHostDefaults(
			whb =>
			{
				whb.ConfigureKestrel(
					options =>
					{
						options.ListenAnyIP(
							42069,
							configure =>
							{
								configure.UseHttps(
									httpsOptions =>
									{
										if (certificate is not null)
											httpsOptions.ServerCertificate = certificate;

										httpsOptions.AllowAnyClientCertificate();
									}
								);
							}
						);
					}
				);

				whb.UseStartup<KestrelStartup>();
			}
		);

		return builder.Build();
	}

	private static X509Certificate2? ConfigureCertificate()
	{
		X509Certificate2 certificate = null;

		if (!Directory.Exists(ClientPaths.CertDirectory))
			Directory.CreateDirectory(ClientPaths.CertDirectory);

		try
		{
			certificate = new X509Certificate2(ClientPaths.CertPath);
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Could not load certificate - defaulting to dev certs");

			MessageBox.Show(
				"Could not load certificate. " +
				"Make sure a valid host certificate is installed as ./cert/cert.pfx. Defaulting to dev certs. " +
				"CAUTION: This might make the application behave in unexpected ways",
				"Error",
				MessageBoxButton.OK,
				MessageBoxImage.Error
			);
		}

		return certificate;
	}
}