using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Innkeep.Core.Constants;
using Innkeep.Startup.Services;
using MudBlazor.Services;
using Serilog;

namespace Innkeep.Server.Startup;

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
				// add all server services
				ServerServiceManager.ConfigureServices(services, true);

				// add Mudblazor and wpf services
				services.AddWpfBlazorWebView();
				services.AddBlazorWebViewDeveloperTools();
				services.AddMudServices();

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
							1337,
							configure => configure.UseHttps(
								httpsOptions =>
								{
									if (certificate is not null)
										httpsOptions.ServerCertificate = certificate;

									httpsOptions.AllowAnyClientCertificate();
								}
							)
						);
					}
				);

				whb.UseStartup<KestrelStartup>();
			}
		);

		builder.UseSerilog();

		return builder.Build();
	}

	private static X509Certificate2? ConfigureCertificate()
	{
		X509Certificate2 certificate = null;

		if (!Directory.Exists(ServerPaths.CertDirectory))
			Directory.CreateDirectory(ServerPaths.CertDirectory);

		try
		{
			certificate = new X509Certificate2(ServerPaths.CertPath);
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