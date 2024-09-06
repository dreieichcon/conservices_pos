using Innkeep.Api.Endpoints;
using Innkeep.Api.Internal.Interfaces.Server.Pos;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Api.Models.Internal;
using Innkeep.Db.Client.Models;
using Innkeep.Http.Interfaces;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Pos;

public class SalesItemRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ISalesItemRepository
{
	public async Task<IHttpResponse<IEnumerable<DtoSalesItem>>> GetSalesItems()
	{
		var baseUri = await GetAddress();
		var uri = new ServerEndpointBuilder(baseUri).WithItems().GetAll().WithIdentifier(Identifier).Build();

		var response = await Get(uri);

		return DeserializeResult<IEnumerable<DtoSalesItem>>(response);
	}
}