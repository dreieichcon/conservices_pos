using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Internal;
using Innkeep.Server.Controllers.Abstract;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("checkin")]
public class CheckinController(IRegisterService registerService, IPretixCheckinService checkinService) : AbstractServerController(registerService)
{
	[HttpPost]
	[Route("entry")]
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
		
		var json = JsonSerializer.Serialize(
			result,
			SerializerOptions.GetServerOptions()
		);

		return new OkObjectResult(json);
	}
}