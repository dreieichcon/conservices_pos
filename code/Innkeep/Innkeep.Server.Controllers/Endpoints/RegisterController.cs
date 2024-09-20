using System.ComponentModel.DataAnnotations;
using Innkeep.Services.Server.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("register")]
public class RegisterController(IRegisterService registerService) : Controller
{
	[HttpGet]
	[Route("discover")]
	public IActionResult Discover() => new OkResult();

	[HttpGet]
	[Route("connect")]
	public async Task<IActionResult> Connect(
		[FromForm] [Length(11, 13)] string identifier,
		[FromForm] string description,
		[FromForm] string hostname
	)
	{
		if (!ModelState.IsValid)
			return new BadRequestResult();

		Log.Debug("Received Connection Request from Register: {Identifier} at {Hostname}", identifier, hostname);

		if (registerService.IsKnown(identifier))
		{
			Log.Debug("Register {Identifier} found in trusted clients", identifier);
			await registerService.Update(identifier, description, hostname);
			return new OkObjectResult(identifier);
		}

		registerService.AddPending(identifier, description);
		Log.Debug("Register {Identifier} is not trusted. Please confirm the connection", identifier);
		return new UnauthorizedResult();
	}
}