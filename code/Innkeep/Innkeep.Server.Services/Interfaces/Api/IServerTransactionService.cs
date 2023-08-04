using Innkeep.Core.DomainModels.Print;
using Innkeep.Models.Printer;
using Innkeep.Server.Data.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Interfaces;

public interface IServerTransactionService
{
	public Task<Receipt> CreateTransaction(PretixTransaction transaction, Register register);
}