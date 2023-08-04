using Innkeep.Server.Controllers.Endpoints.Pretix;
using Innkeep.Server.Controllers.Endpoints.Register;
using Innkeep.Server.Controllers.Endpoints.Transaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Server.Controllers;

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