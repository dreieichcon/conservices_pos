using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;


public class RegisterController : Controller
{
	[HttpGet]
	[Route("register/discover")]
	public IActionResult Discover()
	{
		return new OkObjectResult("I am a register server.");
	}
}