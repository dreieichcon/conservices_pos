using Innkeep.Startup.Authentication;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Discord;

namespace Innkeep.Startup.Services;

public static class LoggingManager
{
	public static void InitializeLogger(string serviceName)
	{
		var auth = new Auth();
		
		Log.Logger = new LoggerConfiguration()
					.WriteTo.Trace()
					.WriteTo.Debug()
					.WriteTo.Console()
					.WriteTo.File("./log/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel:LogEventLevel.Information)
					.WriteTo.Discord(LogEventLevel.Warning, config =>
					{
						config.WebhookUrl = auth.WebhookUrl;
						config.ServiceName = serviceName;
					})
					.Enrich.FromLogContext()
					.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
					.CreateLogger();
	}
}