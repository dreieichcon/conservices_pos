using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.Fiskaly.Response;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public partial class AbstractFiskalyRepository
{
	protected async Task<IHttpResponse<bool>> AuthenticateAdmin(string tssId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).AdminAuth();

		var content = new FiskalyAdminAuth
		{
			AdminPin = authenticationService.CurrentConfig.TseAdminPassword!,
		};

		var result = await Post<FiskalyAdminAuth, FiskalyEmpty>(uri, content);

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}

	protected async Task<IHttpResponse<bool>> LogoutAdmin(string tssId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).AdminLogout();

		var result = await Post<FiskalyEmpty, FiskalyEmpty>(uri, new FiskalyEmpty());

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}
}