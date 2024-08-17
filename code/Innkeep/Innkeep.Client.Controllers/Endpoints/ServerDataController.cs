using Innkeep.Services.Client.Interfaces.Pos;
using Innkeep.Services.Interfaces.Hardware;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Client.Controllers.Endpoints;

[Route("client")]
public class ServerDataController(IHardwareService hardwareService, ISalesItemService salesItemService) : Controller
{
	[HttpPost]
	[Route("print")]
	public IActionResult Print()
	{
		return new OkObjectResult("");
	}

	[HttpPost]
	[Route("reload")]
	public async Task<IActionResult> Reload(string identifier)
	{
		if (!ModelState.IsValid)
			return new BadRequestResult();
		
		if (!IsValid(identifier))
			return new UnauthorizedResult();

		await salesItemService.Load();
		return new OkResult();
	}

	private bool IsValid(string identifier)
	{
		return hardwareService.ClientIdentifier == identifier;
	}
}