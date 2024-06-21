using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Fiskaly.Interfaces.Auth;

public interface IFiskalyAuthRepository
{
	public Task<FiskalyTokenResponse?> Authenticate(AuthenticationInfo authenticationInfo);
}