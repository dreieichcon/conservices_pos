using Innkeep.Server.Services.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Controllers.Abstract;

public abstract class AbstractServerController(IRegisterService registerService) : Controller
{
	public bool IsKnown(string identifier)
	{
		Log.Debug("Received Request from Register: {Identifier}", identifier);
		
		if (registerService.IsKnown(identifier))
		{
			return true;
		}

		Log.Error("Client {Identifier} is not trusted.", identifier);
		return false;
	}
}