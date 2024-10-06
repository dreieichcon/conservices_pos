using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Endpoints.Client;
using Innkeep.Api.Internal.Interfaces.Client.Printing;
using Innkeep.Api.Internal.Repositories.Client.Core;
using Innkeep.Api.Models.General;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Api.Internal.Repositories.Client.Printing;

public class ClientPrintRepository : AbstractClientRepository, IClientPrintRepository
{
	public async Task<IHttpResponse<bool>> PrintReceipt(TransactionReceipt receipt, string identifier, string address)
	{
		var uri = ClientUrlBuilder
				.Endpoints.ClientAddress(address)
				.Print.Transaction.Parameters.Identifier(identifier)
				.Build();

		var result = await Post<TransactionReceipt, Empty>(uri, receipt);

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}

	public async Task<IHttpResponse<bool>> PrintReceipt(
		TransferReceipt receipt,
		string identifier,
		string address,
		string? currency = null
	)
	{
		if (currency != null)
			receipt.Currency = currency;

		var uri = ClientUrlBuilder
				.Endpoints.ClientAddress(address)
				.Print.Transfer.Parameters.Identifier(identifier)
				.Build();

		var result = await Post<TransferReceipt, Empty>(uri, receipt);

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}
}