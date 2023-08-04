using Innkeep.Server.Services.Interfaces.Db;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Controllers.Endpoints.Register;

public class RegisterDetectionController
{
	private readonly IRegisterService _registerService;

	public RegisterDetectionController(IRegisterService registerService)
	{
		_registerService = registerService;
	}
	
	[Route("Register/Discover")]
	public IActionResult Discover()
	{
		return new OkObjectResult("I AM A SERVER");
	}
	
	[Route("Register/Connect/{registerId}")]
	public IActionResult Connect([FromRoute] string registerId)
	{
		Log.Debug("Received Connection Request from Register: {RegisterId}", registerId);

		if (_registerService.CurrentRegistersContains(registerId))
		{
			Log.Debug("Register {RegisterId} found in trusted clients", registerId);
			return new OkObjectResult(registerId);
		}
		
		_registerService.AddPendingRegister(registerId);
		Log.Debug("Register {RegisterId} not trusted", registerId);
		Log.Debug("Please confirm register via register management");

		return new UnauthorizedResult();
	}
}