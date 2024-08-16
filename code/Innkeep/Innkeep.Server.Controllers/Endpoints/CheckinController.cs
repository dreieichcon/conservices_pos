using Innkeep.Api.Models.Internal;
using Innkeep.Server.Controllers.Abstract;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;

public class CheckinController(IRegisterService registerService, IPretixCheckinService checkinService) : AbstractServerController(registerService)
{
	[HttpPost]
	[Route("checkin")]
	public async Task<IActionResult> Checkin(string identifier, [FromBody] CheckinRequest? request)
	{
		if (!ModelState.IsValid) return new BadRequestResult();
		if (!IsKnown(identifier)) return new UnauthorizedResult();
		if (request is null) return new BadRequestResult();

		var result = await checkinService.CheckIn(request);

		if (result is null)
		{
			return new StatusCodeResult(500);
		}
		
		return new OkObjectResult(result);
	}
}