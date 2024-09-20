using Innkeep.Api.Endpoints.Server;
using Innkeep.Api.Internal.Interfaces.Server.Pos;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Pos;

public class TransactionRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ITransactionRepository
{
	public async Task<IHttpResponse<TransactionReceipt>> CommitTransaction(ClientTransaction transaction)
	{
		var baseUri = await GetAddress();

		var uri = ServerUrlBuilder
				.Endpoints.Address(baseUri)
				.Transaction.Create.Parameters.Identifier(Identifier)
				.Build();

		return await Post<ClientTransaction, TransactionReceipt>(uri, transaction);
	}
}