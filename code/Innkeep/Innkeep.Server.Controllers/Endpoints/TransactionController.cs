using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Internal;
using Innkeep.Resources;
using Innkeep.Server.Controllers.Abstract;
using Innkeep.Services.Server.Interfaces.Fiskaly;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Services.Server.Interfaces.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("transaction")]
public class TransactionController : AbstractServerController
{
	private readonly IFiskalyTransactionService _transactionService;
	private readonly IPretixOrderService _orderService;
	private readonly ITransactionService _transactionDbService;

	public TransactionController(
		IFiskalyTransactionService transactionService,
		IPretixOrderService orderService,
		IRegisterService registerService,
		ITransactionService transactionDbService
	) : base(registerService)
	{
		ThreadCultureHelper.SetInvariant();
		_transactionService = transactionService;
		_orderService = orderService;
		_transactionDbService = transactionDbService;
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

		if (fiskalyTransaction is null)
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
		
		await _transactionDbService.CreateFromOrder(pretixOrder, fiskalyTransaction, transaction, json);
		
		return new OkObjectResult(json);
	}
}