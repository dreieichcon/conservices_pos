namespace Innkeep.Server.Startup;

public class KestrelStartup
{
	public void ConfigureServices(IServiceCollection services)
	{
		
	}

	public void Configure(IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseAuthentication();
		
		app.UseRouting();
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		
		app.UseEndpoints(routes => routes.MapControllers());
	}
}