using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Http.Interfaces;

namespace Innkeep.Api.Internal.Interfaces.Server.Pos;

public interface ITransactionRepository
{
	public Task<IHttpResponse<TransactionReceipt>> CommitTransaction(ClientTransaction transaction);
}