using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Pretix.Interfaces.Auth;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Auth;

public class PretixAuthenticationRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixAuthenticationRepository
{
	public async Task<IHttpResponse<bool>> Authenticate()
	{
		var uri = new PretixEndpointBuilder().WithOrganizers().Build();
		var result = await Get(uri);

		return HttpResponse<bool>.Parse(result, result.IsSuccess);
	}
}