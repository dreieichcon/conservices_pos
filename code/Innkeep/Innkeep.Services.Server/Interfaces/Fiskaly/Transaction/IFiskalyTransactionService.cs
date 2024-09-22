using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Services.Server.Interfaces.Fiskaly.Transaction;

public interface IFiskalyTransactionService
{
	public void FinalizeTransactionFlow();

	public Task<FiskalyTransaction?> StartTransaction();

	public Task<TransactionReceipt> CompleteReceiptTransaction(ClientTransaction transaction);

	public Task<TransferReceipt> CompleteTransferTransaction(ClientTransfer model);
}