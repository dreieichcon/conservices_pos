using Microsoft.AspNetCore.Mvc;

namespace Innkeep.Server.Api.Transaction;

public class TransactionRequestController
{
	[Route("/Register/Transaction/{registerId}")]
	[HttpPost]
	public IActionResult Transaction([FromRoute] string registerId, [FromBody] IEnumerable<object> transaction)
	{
		// register sends the entire cart as a transaction request
		// do logic here (create new order at pretix)
		// potentially create a TSE ID
		// store pretix order in database
		// return fully formatted receipt
		return new OkResult();
	}
}