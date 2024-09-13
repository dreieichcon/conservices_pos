using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Core.DomainModels.Authentication;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Fiskaly.Interfaces.Auth;

public interface IFiskalyAuthenticationRepository
{
	public Task<IHttpResponse<FiskalyTokenResponse>> Authenticate(AuthenticationInfo authenticationInfo);
}