using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Db.Server.Models.Transaction;

namespace Innkeep.Services.Server.Interfaces.Transaction;

public interface ITransactionService
{
	public IList<TransactionModel> PendingTransactions { get; set; }
	
	public Task<IEnumerable<TransactionModel>> GetAll();

	public Task<IEnumerable<TransactionModel>> GetForRegister(string identifier);

	public Task<Dictionary<string, decimal>> GetAllCashStates();

	public Task SavePending();

	public Task<TransactionModel?> CreateFromOrder(
		PretixOrderResponse pretixOrder,
		FiskalyTransaction? fiskalyTransaction,
		ClientTransaction transaction,
		string identifier,
		string receiptJson
	);

	public Task<string?> CreateFromTransfer(
		ClientTransfer transfer,
		FiskalyTransaction? transaction,
		string identifier,
		string receiptJson
	);
}