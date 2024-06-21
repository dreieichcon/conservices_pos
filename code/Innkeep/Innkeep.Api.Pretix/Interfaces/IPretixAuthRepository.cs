namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixAuthRepository
{
	public Task<bool> Authenticate();
}