using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Server.Controllers.Abstract;
using Innkeep.Services.Server.Interfaces.Fiskaly.Transaction;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;
using Innkeep.Services.Server.Interfaces.Transaction;
using Innkeep.Strings;
using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Controllers.Endpoints;

[Route("transaction")]
public class TransactionController : AbstractServerController
{
	private readonly IPretixOrderService _orderService;
	private readonly IPretixSalesItemService _salesItemService;
	private readonly ITransactionService _transactionDbService;
	private readonly IFiskalyTransactionService _transactionService;

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
	public async Task<IActionResult> Create([FromQuery] string identifier, [FromBody] ClientTransaction? transaction)
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

		var receipt = await _transactionService.CompleteReceiptTransaction(transaction);

		receipt.Title = pretixOrder.EventTitle;
		receipt.Header = pretixOrder.ReceiptHeader;
		receipt.Currency = transaction.SalesItems.First().Currency;

		receipt.ReceiptVouchers = GenerateVouchers(pretixOrder, transaction);

		var json = JsonSerializer.Serialize(receipt, SerializerOptions.GetServerOptions());

		var item = await _transactionDbService.CreateFromOrder(
			pretixOrder,
			fiskalyTransaction,
			transaction,
			identifier,
			json
		);

		receipt.TransactionId = item?.Id ?? "";
		receipt.BookingTime = item?.TransactionDate ?? DateTime.Now;

		var newJson = JsonSerializer.Serialize(receipt, SerializerOptions.GetServerOptions());

		return new OkObjectResult(newJson);
	}

	private static List<ReceiptVoucher> GenerateVouchers(PretixOrderResponse pretixOrder, ClientTransaction transaction)
	{
		var lst = new List<ReceiptVoucher>();

		foreach (var position in pretixOrder.OrderPositions)
		{
			var existingSalesItem = transaction.SalesItems.First(x => x.Id == position.Item);

			if (existingSalesItem.PrintCheckinVoucher)
				lst.Add(
					new ReceiptVoucher
					{
						ItemName = existingSalesItem.Name,
						Secret = position.Secret,
					}
				);
		}

		return lst;
	}
}