using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Api.Internal.Interfaces.Client.Printing;

public interface IClientPrintRepository
{
	public Task<IHttpResponse<bool>> PrintReceipt(TransactionReceipt receipt, string identifier, string address);

	public Task<IHttpResponse<bool>> PrintReceipt(
		TransferReceipt receipt,
		string identifier,
		string address,
		string? currency = null
	);
}