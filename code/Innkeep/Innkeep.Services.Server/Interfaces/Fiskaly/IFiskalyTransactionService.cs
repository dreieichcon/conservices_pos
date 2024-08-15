using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Server.Interfaces.Fiskaly;

public interface IFiskalyTransactionService
{
	public Task<bool> StartTransaction();

	public Task<TransactionReceipt?> CompleteReceiptTransaction(ClientTransaction transaction);
}