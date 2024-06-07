using Innkeep.Server.Controllers.Legacy.Endpoints.Pretix;
using Innkeep.Server.Controllers.Legacy.Endpoints.Register;
using Innkeep.Server.Controllers.Legacy.Endpoints.Transaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Server.Controllers.Legacy;

public class ServerControllerManager
{
	public static void Initialize(WebApplicationBuilder builder)
	{
		ConfigureServerControllers(builder.Services);
	}

	private static void ConfigureServerControllers(IServiceCollection collection)
	{
		collection.AddScoped<RegisterDetectionController>();
		collection.AddScoped<PretixRequestController>();
		collection.AddScoped<TransactionRequestController>();
	}
}