using Innkeep.Api.Endpoints;
using Innkeep.Api.Internal.Interfaces.Client.Actions;
using Innkeep.Api.Internal.Repositories.Client.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Internal.Repositories.Client.Actions;

public class ClientReloadRepository : AbstractClientRepository, IClientReloadRepository
{
	public async Task<IHttpResponse<bool>> Reload(string identifier, string address)
	{
		var uri = new ClientEndpointBuilder(address).WithClient().Reload().WithIdentifier(identifier).Build();

		var result = await Post(uri, "{}");

		return HttpResponse<bool>.Parse(result, result.IsSuccess);
	}
}