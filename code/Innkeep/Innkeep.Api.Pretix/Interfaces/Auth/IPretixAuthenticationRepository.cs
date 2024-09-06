using Innkeep.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.Auth;

public interface IPretixAuthenticationRepository
{
	public Task<IHttpResponse<bool>> Authenticate();
}