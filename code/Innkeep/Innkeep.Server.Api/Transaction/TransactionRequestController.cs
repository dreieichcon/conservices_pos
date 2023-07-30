using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Innkeep.Server.Api.Binders;
using Innkeep.Server.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Api.Transaction;

[ApiController]
public class TransactionRequestController : ControllerBase
{
	private IRegisterService _registerService;
	private IServerTransactionService _serverTransactionService;

	public TransactionRequestController(IRegisterService registerService, IServerTransactionService serverTransactionService)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		_registerService = registerService;
		_serverTransactionService = serverTransactionService;
	}

	[Route("/Register/Transaction/{registerId}")]
	[HttpPost]
	public async Task<IActionResult> Transaction([FromRoute] string registerId)
	{
		Log.Debug("Received Sales Items Request from Register: {RegisterId}", registerId);

		if (_registerService.CurrentRegistersContains(registerId, out var register))
		{
			Log.Debug("Register {RegisterId} found in trusted clients, accepting transaction", registerId);

			var reader = new StreamReader(Request.Body);
			var body = await reader.ReadToEndAsync();
			var formatted = FormatDecimals(body);
			var transaction = JsonSerializer.Deserialize<Shared.Objects.Transaction.Transaction>(formatted, new JsonSerializerOptions()
			{
				Converters = { new DecimalJsonConverter() }
			});
			var receipt = await _serverTransactionService.CreateTransaction(transaction!, register!);
			return new JsonResult(receipt);
		}
		
		
		Log.Debug("Register {RegisterId} not trusted", registerId);
		return new UnauthorizedResult();
	}

	private static string FormatDecimals(string input)
	{
		var re = new Regex(@"([-]?[0-9]+\.[0-9]+)(?=[,}])");
		return re.Replace(input, "\"$1\"");
	}
}