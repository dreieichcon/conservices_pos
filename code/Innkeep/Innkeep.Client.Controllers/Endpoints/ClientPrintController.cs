using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Client.Controllers.Endpoints;

[Route("client/print")]
public class ClientPrintController(
	IHardwareService hardwareService,
	IPrinterService printerService,
	IDbService<ClientConfig> clientConfigService
) : Controller
{
	[HttpPost]
	[Route("transaction")]
	public IActionResult Print([FromRoute] string identifier, [FromBody] TransactionReceipt receipt)
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
	public IActionResult PrintTransfer([FromRoute] string identifier, [FromBody] TransferReceipt receipt)
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
		=> hardwareService.ClientIdentifier == identifier;
}