using Innkeep.Api.Models.Fiskaly;
using Innkeep.Api.Models.Fiskaly.Response;

namespace Innkeep.Api.Interfaces.Repository.Auth;

public interface IFiskalyAuthRepository
{
	public Task<FiskalyTokenResponse?> Authenticate();
}