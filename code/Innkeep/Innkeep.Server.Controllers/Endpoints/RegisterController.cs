using Innkeep.Server.Services.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("register")]
public class RegisterController(IRegisterService registerService) : Controller
{
	[HttpGet]
	[Route("discover")]
	public IActionResult Discover()
	{
		return new OkObjectResult("I am a register server.");
	}

	[HttpGet]
	[Route("connect")]
	public IActionResult Connect(string identifier, string description)
	{
		Log.Debug("Received Connection Request from Register: {Identifier}", identifier);

		if (registerService.IsKnown(identifier))
		{
			Log.Debug("Register {Identifier} found in trusted clients", identifier);
			return new OkObjectResult(identifier);
		}

		registerService.AddPending(identifier, description);
		Log.Debug("Register {Identifier} is not trusted. Please confirm the connection", identifier);
		return new UnauthorizedResult();
	}
}