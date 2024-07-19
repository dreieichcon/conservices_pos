using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Client.Startup;

public class KestrelStartup
{
	public void ConfigureServices(IServiceCollection services)
	{
		
	}
	
	public void Configure(IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();
		
		app.UseRouting();
		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseAuthentication();
		
		app.UseEndpoints(routes => routes.MapControllers());
	}
}