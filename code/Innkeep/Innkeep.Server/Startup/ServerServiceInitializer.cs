using Innkeep.Server.Services.Interfaces.Pretix;
using Innkeep.Server.Services.Interfaces.Registers;

namespace Innkeep.Server.Startup;

public static class ServerServiceInitializer
{
	public static void InitializeServices(IServiceProvider provider)
	{
		provider.GetRequiredService<IPretixSalesItemService>();
		provider.GetRequiredService<IRegisterService>();
	}
}