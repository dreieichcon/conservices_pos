using Innkeep.Api.Fiskaly.Legacy.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Fiskaly.Legacy.Interfaces;

public interface IFiskalyTransactionRepository
{
	public Task<TransactionResponseModel?> StartTransaction(PretixTransaction pretixTransaction);

	public Task<TransactionResponseModel?> StartFromCashFlow(Guid transactionId);

	public Task<TransactionResponseModel?> UpdateTransaction(TransactionUpdateRequestModel requestModel, string transactionId);

	public Task<TransactionResponseModel?> EndTransaction(TransactionUpdateRequestModel requestModel, string transactionId);
}