using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Api.Register;

public class RegisterDetectionController
{
	[Route("Register/Discover")]
	public IActionResult Discover()
	{
		return new OkObjectResult("I AM A SERVER");
	}
	
	[Route("Register/Connect/{registerId}")]
	public IActionResult Connect([FromRoute] string registerId)
	{
		Trace.WriteLine(registerId);

		// TODO - Check MAC against trusted items in DB
		// If successful return pretix items
		// else return unauthorized
		
		return new OkObjectResult(registerId);
	}
}