using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Fiskaly.Data;

public static class TransactionStartSerializer
{
	public static TransactionStartRequestModel CreateTransactionStart(PretixTransaction transaction, string clientId)
	{
		return new TransactionStartRequestModel()
		{
			ClientId = Guid.Parse(clientId),
			State = TransactionState.ACTIVE,
		};
	}
}