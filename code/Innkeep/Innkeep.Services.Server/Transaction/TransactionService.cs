using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Models.Transaction;
using Innkeep.Services.Server.Interfaces.Transaction;

namespace Innkeep.Services.Server.Transaction;

public class TransactionService(IDbRepository<TransactionModel> transactionRepository) : ITransactionService
{
	
	public async Task<bool> CreateFromOrder(
		PretixOrderResponse pretixOrder, 
		FiskalyTransaction fiskalyTransaction, 
		ClientTransaction transaction,
		string receiptJson)
	{
		var model = new TransactionModel()
		{
			OperationType = Operation.Created,
			TransactionDate = DateTime.Now,
			ReceiptType = "RECEIPT",
			TssId = fiskalyTransaction.TssId,
			ClientId = fiskalyTransaction.ClientId,
			EventId = pretixOrder.EventId,
			OrderSecret = pretixOrder.Secret,
			AmountRequested = transaction.AmountNeeded,
			AmountGiven = transaction.AmountGiven,
			AmountBack = transaction.AmountBack,
			ReceiptJson = receiptJson,
		};
		
		var result = await transactionRepository.CrudAsync(model);

		return result.Success;
	}
}