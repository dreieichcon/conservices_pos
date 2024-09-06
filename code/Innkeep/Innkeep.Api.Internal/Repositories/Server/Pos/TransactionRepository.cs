using Innkeep.Api.Endpoints;
using Innkeep.Api.Internal.Interfaces.Server.Pos;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Db.Client.Models;
using Innkeep.Http.Interfaces;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Pos;

public class TransactionRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ITransactionRepository
{
	public async Task<IHttpResponse<TransactionReceipt>> CommitTransaction(ClientTransaction transaction)
	{
		var baseUri = await GetAddress();

		var uri = new ServerEndpointBuilder(baseUri).WithTransaction().Create().WithIdentifier(Identifier).Build();

		var serialized = Serialize(transaction);

		var result = await Post(uri, serialized);

		return DeserializeResult<TransactionReceipt>(result);
	}
}