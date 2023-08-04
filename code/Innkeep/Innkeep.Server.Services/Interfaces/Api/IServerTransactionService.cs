using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Services.Interfaces.Api;

public interface IServerTransactionService
{
	public Task<Receipt> CreateTransaction(PretixTransaction transaction, Register register);
}