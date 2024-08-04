using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;

namespace Innkeep.Server.Startup;

public static class ServerServiceInitializer
{
	public static async Task InitializeServices(IServiceProvider provider)
	{
		provider.GetRequiredService<IPretixSalesItemService>();
		await provider.GetRequiredService<IRegisterService>().Load();
	}
}