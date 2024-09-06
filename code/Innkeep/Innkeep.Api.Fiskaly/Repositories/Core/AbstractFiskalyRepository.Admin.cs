using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public abstract partial class AbstractFiskalyRepository
{
	protected async Task<IHttpResponse<bool?>> AuthenticateAdmin(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithAdminAuth().Build();

		var content = Serialize(
			new FiskalyAdminAuthenticationRequest
			{
				AdminPin = authenticationService.CurrentConfig.TseAdminPassword!,
			}
		);

		var result = await Post(endpoint, content);

		return HttpResponse<bool?>.Parse(result, result.IsSuccess);
	}

	protected async Task<IHttpResponse<bool?>> LogoutAdmin(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithAdminLogout().Build();

		var result = await Post(endpoint, "{}");

		return HttpResponse<bool?>.Parse(result, result.IsSuccess);
	}
}