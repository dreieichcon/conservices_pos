using Innkeep.Api.Fiskaly.Legacy.Enums;
using Innkeep.Api.Fiskaly.Legacy.Models;

namespace Innkeep.Api.Fiskaly.Legacy.Data;

public static class TransactionStartSerializer
{
	public static TransactionStartRequestModel CreateTransactionStart(string clientId)
	{
		return new TransactionStartRequestModel()
		{
			ClientId = Guid.Parse(clientId),
			State = TransactionState.ACTIVE,
		};
	}
}