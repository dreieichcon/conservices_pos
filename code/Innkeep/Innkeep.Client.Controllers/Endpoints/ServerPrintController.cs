using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Client.Controllers.Endpoints;

[Route("print")]
public class ServerPrintController(
	IHardwareService hardwareService, 
	IPrinterService printerService,
	IDbService<ClientConfig> clientConfigService) : Controller
{
	[HttpPost]
	[Route("transaction")]
	public IActionResult Print(string identifier, [FromBody] TransactionReceipt receipt)
	{
		if (!ModelState.IsValid)
			return new BadRequestResult();
		
		if (!IsValid(identifier))
			return new UnauthorizedResult();

		var printer = clientConfigService.CurrentItem!.PrinterName;
		printerService.PrintReceipt(printer, receipt);

		return new OkResult();
	}
	
	[HttpPost]
	[Route("transfer")]
	public IActionResult PrintTransfer(string identifier, [FromBody] TransferReceipt receipt)
	{
		if (!ModelState.IsValid)
			return new BadRequestResult();
		
		if (!IsValid(identifier))
			return new UnauthorizedResult();

		var printer = clientConfigService.CurrentItem!.PrinterName;
		printerService.PrintReceipt(printer, receipt);

		return new OkResult();
	}
	
	private bool IsValid(string identifier)
	{
		return hardwareService.ClientIdentifier == identifier;
	}
}