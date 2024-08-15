using Innkeep.Api.Models.Internal;

namespace Innkeep.Api.Server.Interfaces;

public interface ITransactionRepository
{
	public Task<TransactionReceipt?> CommitTransaction(ClientTransaction transaction);
}