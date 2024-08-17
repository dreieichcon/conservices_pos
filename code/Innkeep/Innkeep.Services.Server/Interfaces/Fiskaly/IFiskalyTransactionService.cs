using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Services.Server.Interfaces.Fiskaly;

public interface IFiskalyTransactionService
{
	public Task<FiskalyTransaction?> StartTransaction();

	public Task<TransactionReceipt?> CompleteReceiptTransaction(ClientTransaction transaction);

	public Task<TransferReceipt?> CompleteTransferTransaction();
}