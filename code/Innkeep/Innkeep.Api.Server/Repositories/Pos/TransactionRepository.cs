using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Server.Interfaces;
using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Server.Repositories.Pos;

public class TransactionRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ITransactionRepository
{
	public async Task<TransactionReceipt?> CommitTransaction(ClientTransaction transaction)
	{
		var baseUri = await GetAddress();

		var uri = new ServerEndpointBuilder(baseUri).WithTransaction().Create().WithIdentifier(Identifier).Build();

		var serialized = Serialize(transaction);

		var result = await Post(uri, serialized);

		return DeserializeResult<TransactionReceipt>(result);
	}
}