using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;
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
	private readonly IPretixSalesItemService _salesItemService;

	public TransactionController(
		IFiskalyTransactionService transactionService,
		IPretixOrderService orderService,
		IRegisterService registerService,
		ITransactionService transactionDbService,
		IPretixSalesItemService salesItemService
	) : base(registerService)
	{
		ThreadCultureHelper.SetInvariant();
		_transactionService = transactionService;
		_orderService = orderService;
		_transactionDbService = transactionDbService;
		_salesItemService = salesItemService;
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

		if (transaction.SalesItems.Any(x => !x.Infinite)) 
			await _salesItemService.LoadQuotas();

		var fiskalyTransaction = await _transactionService.StartTransaction();

		if (fiskalyTransaction is null)
			return new StatusCodeResult(500);

		var receipt = await _transactionService.CompleteReceiptTransaction(transaction);
		
		receipt.Title = pretixOrder.EventTitle;
		receipt.Header = pretixOrder.ReceiptHeader;
		receipt.Currency = transaction.SalesItems.First().Currency;
		
		var json = JsonSerializer.Serialize(
			receipt,
			SerializerOptions.GetServerOptions()
		);
		
		var item = await _transactionDbService.CreateFromOrder(pretixOrder, fiskalyTransaction, transaction, identifier, json);

		receipt.TransactionId = item?.Id ?? "";
		receipt.BookingTime = item?.TransactionDate ?? DateTime.Now;

		var newJson = JsonSerializer.Serialize(receipt, SerializerOptions.GetServerOptions());
		
		return new OkObjectResult(newJson);
	}
}