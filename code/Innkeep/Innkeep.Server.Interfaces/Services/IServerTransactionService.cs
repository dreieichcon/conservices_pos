using Innkeep.Core.DomainModels.Print;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.Server.Interfaces.Services;

public interface IServerTransactionService
{
	public Task<Receipt> CreateTransaction(Transaction transaction);
}