using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;

namespace Innkeep.Api.Server.Interfaces;

public interface ITransactionRepository
{
	public Task<TransactionReceipt?> CommitTransaction(ClientTransaction transaction);
}