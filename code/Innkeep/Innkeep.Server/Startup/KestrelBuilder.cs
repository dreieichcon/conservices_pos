using Innkeep.Startup.Services;

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

		builder.ConfigureWebHostDefaults(
			whb =>
			{
				whb.ConfigureKestrel(options => options.ListenAnyIP(1337, configure => configure.UseHttps()));
				whb.UseStartup<KestrelStartup>();
			}
		);

		return builder.Build();
	}
}