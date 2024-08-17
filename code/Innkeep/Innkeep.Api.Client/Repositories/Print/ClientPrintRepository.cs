using Innkeep.Api.Client.Interfaces;
using Innkeep.Api.Client.Repositories.Core;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
using Innkeep.Services.Server.Interfaces.Pretix;
using Innkeep.Services.Server.Interfaces.Registers;

namespace Innkeep.Api.Client.Repositories.Print;

public class ClientPrintRepository(IRegisterService registerService, IPretixSalesItemService itemService) : AbstractClientRepository(registerService), IClientPrintRepository
{
	public async Task<bool> PrintReceipt(TransactionReceipt receipt, string identifier)
	{
		var address = await GetAddress(identifier);
		
		var uri = new ClientEndpointBuilder(address).WithPrint().Transaction().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return result.IsSuccess;
	}

	public async Task<bool> PrintReceipt(TransferReceipt receipt, string identifier)
	{
		var address = await GetAddress(identifier);

		receipt.Currency = itemService.DtoSalesItems.First().Currency;
		var uri = new ClientEndpointBuilder(address).WithPrint().Transfer().WithIdentifier(identifier).Build();

		var json = Serialize(receipt);

		var result = await Post(uri, json);

		return result.IsSuccess;
	}
}