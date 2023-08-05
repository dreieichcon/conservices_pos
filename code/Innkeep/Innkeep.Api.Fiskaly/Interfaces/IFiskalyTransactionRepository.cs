using Innkeep.Api.Fiskaly.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Fiskaly.Interfaces;

public interface IFiskalyTransactionRepository
{
	public Task<TransactionResponseModel?> StartTransaction(PretixTransaction pretixTransaction);

	public Task<TransactionResponseModel?> UpdateTransaction(TransactionUpdateRequestModel requestModel, string transactionId);

	public Task<TransactionResponseModel?> EndTransaction(TransactionUpdateRequestModel requestModel, string transactionId);
}