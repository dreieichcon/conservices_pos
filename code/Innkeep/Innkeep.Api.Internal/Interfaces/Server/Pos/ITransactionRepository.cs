using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Internal.Transaction;

namespace Innkeep.Api.Internal.Interfaces.Server.Pos;

public interface ITransactionRepository
{
	public Task<IHttpResponse<TransactionReceipt>> CommitTransaction(ClientTransaction transaction);
}