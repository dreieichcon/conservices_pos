using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Api.Client.Interfaces;

public interface IClientPrintRepository
{
	public Task<bool> PrintReceipt(TransactionReceipt receipt, string identifier);

	public Task<bool> PrintReceipt(TransferReceipt receipt, string identifier);
}