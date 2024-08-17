using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Models.Transaction;
using Innkeep.Services.Server.Interfaces.Transaction;

namespace Innkeep.Services.Server.Transaction;

public class TransactionService(IDbRepository<TransactionModel> transactionRepository) : ITransactionService
{
	public async Task<IEnumerable<TransactionModel>> GetAll()
	{
		return await transactionRepository.GetAllAsync();
	}

	public async Task<IEnumerable<TransactionModel>> GetForRegister(string identifier)
	{
		return await transactionRepository.GetAllCustomAsync(x => x.RegisterId == identifier);
	}

	public async Task<Dictionary<string, decimal>> GetAllCashStates()
	{
		var items = await GetAll();

		var t = items
				.GroupBy(x => x.RegisterId)
				.ToDictionary(group => group.Key, group => group.Sum(x => x.TotalChange));
		
		return t;
	}

	public async Task<TransactionModel?> CreateFromOrder(
		PretixOrderResponse pretixOrder,
		FiskalyTransaction fiskalyTransaction,
		ClientTransaction transaction,
		string identifier,
		string receiptJson
	)
	{
		var model = new TransactionModel()
		{
			OperationType = Operation.Created,
			TransactionDate = DateTime.Now,
			ReceiptType = "RECEIPT",
			RegisterId = identifier,
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

		return result.Success ? result.Item : null;
	}

	public async Task<string?> CreateFromTransfer(
		ClientTransfer transfer,
		FiskalyTransaction? transaction,
		string identifier,
		string receiptJson
	)
	{
		var model = new TransactionModel()
		{
			TransactionDate = DateTime.Now,
			ReceiptType = "TRANSFER",
			TssId = transaction?.TssId ?? "",
			ClientId = transaction?.ClientId ?? "",
			RegisterId = identifier,
			AmountGiven = transfer.IsRetrieve ? 0 : transfer.Amount,
			AmountBack = transfer.IsRetrieve ? transfer.Amount : 0,
			AmountRequested = 0,
			ReceiptJson = receiptJson,
			OperationType = Operation.Created,
		};

		var result = await transactionRepository.CrudAsync(model);

		return result.Success ? result.Item!.Id : "";
	}
}