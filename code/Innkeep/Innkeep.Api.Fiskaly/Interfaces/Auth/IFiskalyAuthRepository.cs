using Innkeep.Api.Models.Fiskaly.Response;

namespace Innkeep.Api.Fiskaly.Interfaces.Auth;

public interface IFiskalyAuthRepository
{
	public Task<FiskalyTokenResponse?> Authenticate();
}