using Innkeep.Api.Models.Internal.Register;
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

	[HttpPost]
	[Route("connect")]
	public async Task<IActionResult> Connect([FromBody] ConnectionRequest request)
	{
		if (!ModelState.IsValid)
			return new BadRequestResult();

		Log.Debug(
			"Received Connection Request from Register: {Identifier} at {Hostname}",
			request.Identifier,
			request.HostName
		);

		if (registerService.IsKnown(request.Identifier))
		{
			Log.Debug("Register {Identifier} found in trusted clients", request.Identifier);
			await registerService.Update(request.Identifier, request.Description, request.HostName);
			return new OkResult();
		}

		registerService.AddPending(request.Identifier, request.Description);
		Log.Debug("Register {Identifier} is not trusted. Please confirm the connection", request.Identifier);
		return new UnauthorizedResult();
	}
}