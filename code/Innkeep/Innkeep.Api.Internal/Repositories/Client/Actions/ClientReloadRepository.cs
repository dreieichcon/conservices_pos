using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Endpoints.Client;
using Innkeep.Api.Internal.Interfaces.Client.Actions;
using Innkeep.Api.Internal.Repositories.Client.Core;
using Innkeep.Api.Models.General;

namespace Innkeep.Api.Internal.Repositories.Client.Actions;

public class ClientReloadRepository : AbstractClientRepository, IClientReloadRepository
{
	public async Task<IHttpResponse<bool>> Reload(string identifier, string address)
	{
		var uri = ClientUrlBuilder.Endpoints.ClientAddress(address).Reload.Parameters.Identifier(identifier).Build();

		var result = await Post<Empty, Empty>(uri, new Empty());

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}
}