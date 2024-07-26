using Innkeep.Services.Client.Interfaces.Pos;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Client.Startup;

public static class ClientEventInitializer
{
	public static void Initialize(IServiceProvider provider)
	{
		provider.GetRequiredService<ISalesItemService>();
	}
}