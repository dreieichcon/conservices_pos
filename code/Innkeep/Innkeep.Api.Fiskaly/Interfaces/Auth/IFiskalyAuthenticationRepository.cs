using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Fiskaly.Interfaces.Auth;

public interface IFiskalyAuthenticationRepository
{
	public Task<IHttpResponse<FiskalyTokenResponse>> Authenticate(AuthenticationInfo authenticationInfo);
}