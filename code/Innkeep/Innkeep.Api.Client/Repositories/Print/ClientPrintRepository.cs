using Innkeep.Api.Client.Interfaces;
using Innkeep.Api.Client.Repositories.Core;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;


namespace Innkeep.Api.Client.Repositories.Print;

public class ClientPrintRepository : AbstractClientRepository, IClientPrintRepository
{
	public async Task<bool> PrintReceipt(TransactionReceipt receipt, string identifier, string address)
	{
		var uri = new ClientEndpointBuilder(address).WithPrint().Transaction().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return result.IsSuccess;
	}

	public async Task<bool> PrintReceipt(TransferReceipt receipt, string identifier, string address, string? currency = null)
	{
		if (currency != null)
			receipt.Currency = currency;
		
		var uri = new ClientEndpointBuilder(address).WithPrint().Transfer().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return result.IsSuccess;
	}
}