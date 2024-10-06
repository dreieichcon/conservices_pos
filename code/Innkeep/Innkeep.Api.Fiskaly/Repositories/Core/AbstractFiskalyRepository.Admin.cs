using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.General;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public partial class AbstractFiskalyRepository
{
	protected async Task<IHttpResponse<bool>> AuthenticateAdmin(string tssId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.SpecificTss(tssId).AdminAuth;

		var content = new FiskalyAdminAuth
		{
			AdminPin = authenticationService.CurrentConfig.TseAdminPassword!,
		};

		var result = await Post<FiskalyAdminAuth, Empty>(uri, content);

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}

	protected async Task<IHttpResponse<bool>> LogoutAdmin(string tssId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.SpecificTss(tssId).AdminLogout;

		var result = await Post<Empty, Empty>(uri, new Empty());

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}
}