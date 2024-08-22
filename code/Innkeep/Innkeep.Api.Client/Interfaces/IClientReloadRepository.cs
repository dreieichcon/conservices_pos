namespace Innkeep.Api.Client.Interfaces;

public interface IClientReloadRepository
{
	public Task<bool> Reload(string identifier, string address);
}