namespace Innkeep.Api.Interfaces.Repository.Auth;

public interface IPretixAuthRepository
{
	public Task<bool> Authenticate();
}