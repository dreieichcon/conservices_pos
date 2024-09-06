using Innkeep.Api.Endpoints;
using Innkeep.Api.Internal.Interfaces.Client.Printing;
using Innkeep.Api.Internal.Repositories.Client.Core;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Internal.Repositories.Client.Printing;

public class ClientPrintRepository : AbstractClientRepository, IClientPrintRepository
{
	public async Task<IHttpResponse<bool>> PrintReceipt(TransactionReceipt receipt, string identifier, string address)
	{
		var uri = new ClientEndpointBuilder(address).WithPrint().Transaction().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return HttpResponse<bool>.Parse(result, result.IsSuccess);
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

		var uri = new ClientEndpointBuilder(address).WithPrint().Transfer().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return HttpResponse<bool>.Parse(result, result.IsSuccess);
	}
}