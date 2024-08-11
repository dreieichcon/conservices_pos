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
					.WriteTo.Discord(LogEventLevel.Information, config =>
					{
						config.WebhookUrl = auth.WebhookUrl;
						config.ServiceName = serviceName;
					})
					.Enrich.FromLogContext()
					.CreateLogger();
	}
}