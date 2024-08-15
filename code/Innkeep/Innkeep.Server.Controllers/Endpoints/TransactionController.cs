using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Internal;
using Innkeep.Resources;
using Innkeep.Server.Controllers.Abstract;
using Innkeep.Services.Server.Interfaces.Fiskaly;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("transaction")]
public class TransactionController : AbstractServerController
{
	private readonly IFiskalyTransactionService _transactionService;
	private readonly IPretixOrderService _orderService;

	public TransactionController(
		IFiskalyTransactionService transactionService,
		IPretixOrderService orderService,
		IRegisterService registerService
	) : base(registerService)
	{
		ThreadCultureHelper.SetInvariant();
		_transactionService = transactionService;
		_orderService = orderService;
	}

	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> Create(string identifier, [FromBody] ClientTransaction? transaction)
	{
		if (!IsKnown(identifier))
			return new UnauthorizedResult();

		if (!ModelState.IsValid || transaction is null)
			return new BadRequestResult();

		var pretixOrder = await _orderService.CreateOrder(transaction.SalesItems);

		if (pretixOrder is null)
			return new StatusCodeResult(500);

		var fiskalyTransaction = await _transactionService.StartTransaction();

		if (!fiskalyTransaction)
			return new StatusCodeResult(500);

		var receipt = await _transactionService.CompleteReceiptTransaction(transaction);

		if (receipt is null)
			return new StatusCodeResult(500);

		receipt.Title = pretixOrder.EventTitle;
		receipt.Header = pretixOrder.ReceiptHeader;
		receipt.Currency = transaction.SalesItems.First().Currency;
		
		var json = JsonSerializer.Serialize(
			receipt,
			SerializerOptions.GetServerOptions()
		);
		
		return new OkObjectResult(json);
	}
}