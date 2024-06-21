using Innkeep.Api.Fiskaly.Legacy.Models;

namespace Innkeep.Api.Fiskaly.Legacy.Interfaces;

public interface IFiskalyAuthenticationRepository
{
	public Task<AuthenticateApiResponseModel?> AuthenticateApi();
}