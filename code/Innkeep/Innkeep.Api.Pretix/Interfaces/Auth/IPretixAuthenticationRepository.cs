namespace Innkeep.Api.Pretix.Interfaces.Auth;

public interface IPretixAuthenticationRepository
{
	public Task<bool> Authenticate();
}