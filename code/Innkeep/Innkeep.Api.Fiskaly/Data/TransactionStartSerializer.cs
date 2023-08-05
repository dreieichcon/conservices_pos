using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Fiskaly.Data;

public static class TransactionStartSerializer
{
	public static TransactionStartRequestModel CreateTransactionStart(PretixTransaction transaction)
	{
		return new TransactionStartRequestModel()
		{
			ClientId = transaction.TransactionId,
			State = TransactionState.ACTIVE,
		};
	}
}