using Innkeep.Core.DomainModels.Print;
using Innkeep.Server.Data.Models;
using Transaction = Innkeep.Shared.Objects.Transaction.Transaction;

namespace Innkeep.Server.Data.Interfaces;

public interface IServerTransactionService
{
	public Task<Receipt> CreateTransaction(Transaction transaction, Register register);
}