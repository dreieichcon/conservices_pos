using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Json;
using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Legacy.Interfaces.Api;
using Innkeep.Server.Services.Legacy.Interfaces.Db;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innkeep.Server.Controllers.Endpoints.Transaction;

[ApiController]
public class TransactionRequestController : ControllerBase
{
	private readonly IRegisterService _registerService;
	private readonly IServerTransactionService _serverTransactionService;
	private readonly IPretixService _pretixService;

	public TransactionRequestController
		(IRegisterService registerService, IServerTransactionService serverTransactionService, 
		IPretixService pretixService)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		_registerService = registerService;
		_serverTransactionService = serverTransactionService;
		_pretixService = pretixService;
	}
	
	[Route("/Register/CheckIn/{registerId}")]
	[HttpPost]
	public async Task<IActionResult> CheckIn([FromRoute] string registerId)
	{
		Log.Debug("Received CheckIn Request from Register: {RegisterId}", registerId);

		if (_registerService.CurrentRegistersContains(registerId, out var register))
		{
			Log.Debug("Register {RegisterId} found in trusted clients, accepting transaction", registerId);

			var reader = new StreamReader(Request.Body);
			var body = await reader.ReadToEndAsync();
			var formatted = FormatDecimals(body);

			var checkInItem = JsonSerializer.Deserialize<PretixCheckin>(
				formatted,
				new JsonSerializerOptions()
				{
					Converters =
					{
						new DecimalJsonConverter()
					}
				}
			);

			var response = await _pretixService.CheckIn(checkInItem);

			return new JsonResult(response);
		}

		Log.Debug("Register {RegisterId} not trusted", registerId);

		return new UnauthorizedResult();
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

			var transaction = JsonSerializer.Deserialize<PretixTransaction>(
				formatted,
				new JsonSerializerOptions()
				{
					Converters =
					{
						new DecimalJsonConverter()
					}
				}
			);

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