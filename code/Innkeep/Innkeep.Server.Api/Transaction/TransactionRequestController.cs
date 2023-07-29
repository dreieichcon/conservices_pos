using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Api.Transaction;

public class TransactionRequestController
{
	private IRegisterService _registerService;
	private IServerTransactionService _serverTransactionService;

	public TransactionRequestController(IRegisterService registerService, IServerTransactionService serverTransactionService)
	{
		_registerService = registerService;
		_serverTransactionService = serverTransactionService;
	}

	[Route("/Register/Transaction/{registerId}")]
	[HttpPost]
	public async Task<IActionResult> Transaction([FromRoute] string registerId, [FromBody]Shared.Objects.Transaction.Transaction transaction)
	{
		Log.Debug("Received Sales Items Request from Register: {RegisterId}", registerId);

		if (_registerService.CurrentRegistersContains(registerId))
		{
			Log.Debug("Register {RegisterId} found in trusted clients, accepting transaction", registerId);

			var receipt = await _serverTransactionService.CreateTransaction(transaction);
			return new JsonResult(receipt);
		}
		
		Log.Debug("Register {RegisterId} not trusted", registerId);
		return new UnauthorizedResult();
	}
}