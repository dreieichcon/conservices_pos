using Innkeep.Api.Fiskaly.Models;

namespace Innkeep.Api.Fiskaly.Interfaces;

public interface IFiskalyAuthenticationRepository
{
	public Task<AuthenticateApiResponseModel?> AuthenticateApi();
}