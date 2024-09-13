using Flurl.Http;
using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Core.DomainModels.Authentication;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Fiskaly.Repositories.Auth;

public class FiskalyAuthenticationRepository() : AbstractFiskalyRepository(null!), IFiskalyAuthenticationRepository
{
	public async Task<IHttpResponse<FiskalyTokenResponse>> Authenticate(AuthenticationInfo authenticationInfo)
	{
		var request = new FiskalyTokenRequest
		{
			Key = authenticationInfo.Key,
			Secret = authenticationInfo.Secret,
		};

		var uri = FiskalyUrlBuilder.Endpoints.Authenticate();

		var result = await Post<FiskalyTokenRequest, FiskalyTokenResponse>(uri, request);

		return result;
	}

	// Since this repository is there to update the token, the token update call is skipped
	protected override async Task PrepareRequest() => await Task.CompletedTask;

	protected override void AttachPostHeaders(IFlurlRequest request)
	{
		// do nothing here, as we have no token yet
	}
}