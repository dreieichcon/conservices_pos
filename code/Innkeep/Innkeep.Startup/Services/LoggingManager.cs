using Serilog;

namespace Innkeep.Startup.Services;

public class LoggingManager
{
	public static void InitializeLogger()
	{
		Log.Logger = new LoggerConfiguration().WriteTo.Trace().WriteTo.Debug().WriteTo.Console().CreateLogger();
	}
}