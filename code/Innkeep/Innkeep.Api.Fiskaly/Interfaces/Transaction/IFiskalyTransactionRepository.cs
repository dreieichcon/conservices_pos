using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;

namespace Innkeep.Api.Fiskaly.Interfaces.Transaction;

public interface IFiskalyTransactionRepository
{
	public Task<FiskalyTransaction?> StartTransaction(
		string tssId,
		string transactionId,
		string clientId
	);

	public Task<FiskalyTransaction?> UpdateTransaction(FiskalyTransactionUpdateRequest transaction);
}