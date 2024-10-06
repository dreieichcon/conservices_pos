using Demolite.Http.Interfaces;
using Innkeep.Api.Endpoints.Server;
using Innkeep.Api.Internal.Interfaces.Server.Pos;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Api.Models.Internal;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Pos;

public class SalesItemRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ISalesItemRepository
{
	public async Task<IHttpResponse<IEnumerable<DtoSalesItem>>> GetSalesItems()
	{
		var baseUri = await GetAddress();
		var uri = ServerUrlBuilder.Endpoints.Address(baseUri).SalesItems.Parameters.Identifier(Identifier).Build();

		return await Get<IEnumerable<DtoSalesItem>>(uri);
	}
}